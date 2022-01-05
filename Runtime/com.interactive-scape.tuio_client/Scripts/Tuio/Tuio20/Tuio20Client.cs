using System.Collections.Generic;
using System.Linq;
using OSC.NET;
using Tuio.Common;

namespace Tuio.Tuio20
{
    public class Tuio20Client
    {
        private TuioReceiver _tuioReceiver;
        private List<Tuio20Listener> _tuioListeners = new List<Tuio20Listener>();
        private Dictionary<uint, Tuio20Object> _tuioObjects = new Dictionary<uint, Tuio20Object>();

        private OSCMessage _frmMessage = null;
        private List<OSCMessage> _otherMessages = new List<OSCMessage>();
        
        private uint _bundleFrameId = 0;
        private uint _nextFrameId = 0;
        
        private uint _prevFrameId = 0;
        private TuioTime _prevFrameTime = null;

        private uint _dim = 0;
        private string _source = null;
        
        private object _tuioObjectLock = new object();
        
        public Tuio20Client(TuioReceiver tuioReceiver)
        {
            _tuioReceiver = tuioReceiver;
            _tuioReceiver.AddMessageListener("/tuio2/frm", OnFrm);
            _tuioReceiver.AddMessageListener("/tuio2/alv", OnAlv);
            _tuioReceiver.AddMessageListener("/tuio2/tok", OnOther);
            _tuioReceiver.AddMessageListener("/tuio2/ptr", OnOther);
            _tuioReceiver.AddMessageListener("/tuio2/bnd", OnOther);
            _tuioReceiver.AddMessageListener("/tuio2/sym", OnOther);
        }

        public void Connect()
        {
            _prevFrameTime = new TuioTime(0, 0);
            _tuioReceiver.Connect();
        }

        public void Disconnect()
        {
            _tuioReceiver.Disconnect();
        }
        
        public object tuioObjectLock => _tuioObjectLock;
        public uint dim => _dim;
        public string source => _source;
        
        public List<Tuio20Token> GetTuioTokenList()
        {
            lock (_tuioObjectLock)
            {
                var tuioTokenList = new List<Tuio20Token>();
                foreach (var entry in _tuioObjects)
                {
                    if (entry.Value.ContainsTuioToken())
                    {
                        tuioTokenList.Add(entry.Value.token);
                    }
                }

                return tuioTokenList;
            }
        }

        public List<Tuio20Pointer> GetTuioPointerList()
        {
            lock (_tuioObjectLock)
            {
                var tuioPointerList = new List<Tuio20Pointer>();
                foreach (var entry in _tuioObjects)
                {
                    if (entry.Value.ContainsTuioPointer())
                    {
                        tuioPointerList.Add(entry.Value.pointer);
                    }
                }

                return tuioPointerList;
            }
        }

        public List<Tuio20Bounds> GetTuioBoundsList()
        {
            lock (_tuioObjectLock)
            {
                var tuioBoundsList = new List<Tuio20Bounds>();
                foreach (var entry in _tuioObjects)
                {
                    if (entry.Value.ContainsTuioBounds())
                    {
                        tuioBoundsList.Add(entry.Value.bounds);
                    }
                }

                return tuioBoundsList;
            }
        }

        public List<Tuio20Symbol> GetTuioSymbolList()
        {
            lock (_tuioObjectLock)
            {
                var tuioSymbolList = new List<Tuio20Symbol>();
                foreach (var entry in _tuioObjects)
                {
                    if (entry.Value.ContainsTuioSymbol())
                    {
                        tuioSymbolList.Add(entry.Value.symbol);
                    }
                }

                return tuioSymbolList;
            }
        }
        
        private void OnFrm(OSCMessage oscMessage)
        {
            _bundleFrameId = (uint)(int)oscMessage.Values[0];

            if (_bundleFrameId > _nextFrameId)
            {
                _otherMessages.Clear();
                _nextFrameId = _bundleFrameId;
                _frmMessage = oscMessage;
            }
        }
        
        private void OnOther(OSCMessage oscMessage)
        {
            if (_bundleFrameId != _nextFrameId)
            {
                return;
            }

            _otherMessages.Add(oscMessage);
        }

        private void OnAlv(OSCMessage oscMessage)
        {
            if (_frmMessage == null || _bundleFrameId != _nextFrameId)
            {
                return;
            }

            var frameId = (uint)(int)_frmMessage.Values[0];
            var frameTime = (OscTimeTag)_frmMessage.Values[1];
            var dim = (uint)(int)_frmMessage.Values[2];
            var source = (string)_frmMessage.Values[3];
            TuioTime currentFrameTime = TuioTime.FromOscTime(frameTime);
            if (frameId >= _prevFrameId || frameId == 0 || 
                currentFrameTime.Subtract(_prevFrameTime).GetTotalMilliseconds() >= 1000)
            {
                _dim = dim;
                _source = source;
                HashSet<uint> currentSIds = new HashSet<uint>(_tuioObjects.Keys);
                HashSet<uint> aliveSIds = new HashSet<uint>();
                foreach (var sId in oscMessage.Values)
                {
                    aliveSIds.Add((uint)(int) sId);
                }
                HashSet<uint> newSIds = new HashSet<uint>(aliveSIds.Except(currentSIds));
                HashSet<uint> removedSIds = new HashSet<uint>(currentSIds.Except(aliveSIds));
                HashSet<Tuio20Object> addedTuioObjects = new HashSet<Tuio20Object>();
                HashSet<Tuio20Object> updatedTuioObjects = new HashSet<Tuio20Object>();
                HashSet<Tuio20Object> removedTuioObjects = new HashSet<Tuio20Object>();
                lock (_tuioObjectLock)
                {
                    foreach (var sId in newSIds)
                    {
                        var tuioObject = new Tuio20Object(_prevFrameTime, sId);
                        _tuioObjects[sId] = tuioObject;
                    }
                    foreach (var sId in removedSIds)
                    {
                        var tuioObject = _tuioObjects[sId];
                        removedTuioObjects.Add(tuioObject);
                        tuioObject._remove(currentFrameTime);
                        _tuioObjects.Remove(sId);
                    }

                    foreach (var otherOscMessage in _otherMessages)
                    {
                        if (otherOscMessage.Address == "/tuio2/tok")
                        {
                            var sId = (uint)(int)otherOscMessage.Values[0];
                            if (aliveSIds.Contains(sId))
                            {
                                var tuId = (uint)(int)otherOscMessage.Values[1];
                                var cId = (uint)(int)otherOscMessage.Values[2];
                                var xPos = (float)otherOscMessage.Values[3];
                                var yPos = (float)otherOscMessage.Values[4];
                                var angle = (float)otherOscMessage.Values[5];
                                float xVel = 0, yVel = 0, aVel = 0, mAcc = 0, rAcc = 0;
                                if (otherOscMessage.Values.Count > 6)
                                {
                                    xVel = (float)otherOscMessage.Values[6];
                                    yVel = (float)otherOscMessage.Values[7];
                                    aVel = (float)otherOscMessage.Values[8];
                                    mAcc = (float)otherOscMessage.Values[9];
                                    rAcc = (float)otherOscMessage.Values[10];
                                }

                                var tuioObject = _tuioObjects[sId];
                                if (tuioObject.token == null)
                                {
                                    addedTuioObjects.Add(tuioObject);
                                    tuioObject.SetTuioToken(new Tuio20Token(_prevFrameTime, tuioObject, tuId, cId, xPos,
                                        yPos, angle, xVel, yVel, aVel, mAcc, rAcc));
                                }
                                else
                                {
                                    if (tuioObject.token._hasChanged(tuId, cId, xPos,
                                        yPos, angle, xVel, yVel, aVel, mAcc, rAcc))
                                    {
                                        updatedTuioObjects.Add(tuioObject);
                                        tuioObject.token._update(_prevFrameTime, tuId, cId, xPos,
                                            yPos, angle, xVel, yVel, aVel, mAcc, rAcc);
                                    }
                                }
                            }
                        }
                        else if (otherOscMessage.Address == "/tuio2/ptr")
                        {
                            var sId = (uint)(int)otherOscMessage.Values[0];
                            if (aliveSIds.Contains(sId))
                            {
                                var tuId = (uint)(int)otherOscMessage.Values[1];
                                var cId = (uint)(int)otherOscMessage.Values[2];
                                var xPos = (float)otherOscMessage.Values[3];
                                var yPos = (float)otherOscMessage.Values[4];
                                var angle = (float)otherOscMessage.Values[5];
                                var shear = (float)otherOscMessage.Values[6];
                                var radius = (float)otherOscMessage.Values[7];
                                var press = (float)otherOscMessage.Values[8];
                                float xVel = 0, yVel = 0, pVel = 0, mAcc = 0, pAcc = 0;
                                if (otherOscMessage.Values.Count > 9)
                                {
                                    xVel = (float)otherOscMessage.Values[6];
                                    yVel = (float)otherOscMessage.Values[7];
                                    pVel = (float)otherOscMessage.Values[8];
                                    mAcc = (float)otherOscMessage.Values[9];
                                    pAcc = (float)otherOscMessage.Values[10];
                                }

                                var tuioObject = _tuioObjects[sId];
                                if (tuioObject.pointer == null)
                                {
                                    addedTuioObjects.Add(tuioObject);
                                    tuioObject.SetTuioPointer(new Tuio20Pointer(_prevFrameTime, tuioObject, tuId, cId, 
                                        xPos, yPos, angle, shear, radius, press, xVel, yVel, pVel, mAcc, pAcc));
                                }
                                else
                                {
                                    if (tuioObject.pointer._hasChanged(tuId, cId, xPos,
                                        yPos, angle, shear, radius, press, xVel, yVel, pVel, mAcc, pAcc))
                                    {
                                        updatedTuioObjects.Add(tuioObject);
                                        tuioObject.pointer._update(_prevFrameTime, tuId, cId, xPos,
                                            yPos, angle, shear, radius, press, xVel, yVel, pVel, mAcc, pAcc);
                                    }
                                }
                            }
                        }
                        else if (otherOscMessage.Address == "/tuio2/bnd")
                        {
                            var sId = (uint)(int)otherOscMessage.Values[0];
                            if (aliveSIds.Contains(sId))
                            {
                                var xPos = (float)otherOscMessage.Values[1];
                                var yPos = (float)otherOscMessage.Values[2];
                                var angle = (float)otherOscMessage.Values[3];
                                var width = (float)otherOscMessage.Values[4];
                                var height = (float)otherOscMessage.Values[5];
                                var area = (float)otherOscMessage.Values[6];
                                float xVel = 0, yVel = 0, aVel = 0, mAcc = 0, rAcc = 0;
                                if (otherOscMessage.Values.Count > 7)
                                {
                                    xVel = (float)otherOscMessage.Values[7];
                                    yVel = (float)otherOscMessage.Values[8];
                                    aVel = (float)otherOscMessage.Values[9];
                                    mAcc = (float)otherOscMessage.Values[10];
                                    rAcc = (float)otherOscMessage.Values[11];
                                }

                                var tuioObject = _tuioObjects[sId];
                                if (tuioObject.bounds == null)
                                {
                                    addedTuioObjects.Add(tuioObject);
                                    tuioObject.SetTuioBounds(new Tuio20Bounds(_prevFrameTime, tuioObject, xPos,
                                        yPos, angle, width, height, area, xVel, yVel, aVel, mAcc, rAcc));
                                }
                                else
                                {
                                    if (tuioObject.bounds._hasChanged(xPos,
                                        yPos, angle, width, height, area, xVel, yVel, aVel, mAcc, rAcc))
                                    {
                                        updatedTuioObjects.Add(tuioObject);
                                        tuioObject.bounds._update(_prevFrameTime, xPos,
                                            yPos, angle, width, height, area, xVel, yVel, aVel, mAcc, rAcc);
                                    }
                                }
                            }
                        }
                        else if (otherOscMessage.Address == "/tuio2/sym")
                        {
                            var sId = (uint)(int)otherOscMessage.Values[0];
                            if (aliveSIds.Contains(sId))
                            {
                                var tuId = (uint)(int)otherOscMessage.Values[1];
                                var cId = (uint)(int)otherOscMessage.Values[2];
                                string group = (string)otherOscMessage.Values[3];
                                string data = (string)otherOscMessage.Values[4];

                                var tuioObject = _tuioObjects[sId];
                                if (tuioObject.symbol == null)
                                {
                                    addedTuioObjects.Add(tuioObject);
                                    tuioObject.SetTuioSymbol(new Tuio20Symbol(_prevFrameTime, tuioObject, tuId, cId, 
                                        group, data));
                                }
                                else
                                {
                                    if (tuioObject.symbol._hasChanged(tuId, cId, group,
                                        data))
                                    {
                                        updatedTuioObjects.Add(tuioObject);
                                        tuioObject.symbol._update(_prevFrameTime, tuId, cId, group,
                                            data);
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (var tuioObject in addedTuioObjects)
                {
                    foreach (var tuioListener in _tuioListeners)
                    {
                        tuioListener.TuioAdd(tuioObject);
                    }
                }

                foreach (var tuioObject in updatedTuioObjects)
                {
                    foreach (var tuioListener in _tuioListeners)
                    {
                        tuioListener.TuioUpdate(tuioObject);
                    }
                }

                foreach (var tuioObject in removedTuioObjects)
                {
                    foreach (var tuioListener in _tuioListeners)
                    {
                        tuioListener.TuioRemove(tuioObject);
                    }
                }
                
                foreach (var tuioListener in _tuioListeners)
                {
                    tuioListener.TuioRefresh(_prevFrameTime);
                }
            }
            _prevFrameTime = currentFrameTime;
            _prevFrameId = frameId;
            _frmMessage = null;
        }

        public void AddTuioListener(Tuio20Listener tuio20Listener)
        {
            _tuioListeners.Add(tuio20Listener);
        }

        public void RemoveTuioListener(Tuio20Listener tuio20Listener)
        {
            _tuioListeners.Remove(tuio20Listener);
        }

        public void RemoveAllTuioListeners()
        {
            _tuioListeners.Clear();
        }
    }
}