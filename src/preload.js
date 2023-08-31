const { contextBridge, ipcRenderer } = require('electron');

const validSendChannels = ['open-unity-window', 'close-unity-window'];
const validReceiveChannels = ['channel_from_main'];

// Expose ipcRenderer to the client
contextBridge.exposeInMainWorld('ipcRenderer', {
  send: (channel, data) => {
    if (validSendChannels.includes(channel)) {
      ipcRenderer.send(channel, data);
    }
  },
  receive: (channel, func) => {
    if (validReceiveChannels.includes(channel)) {
      // Deliberately strip event as it includes `sender`
      ipcRenderer.on(channel, (event, ...args) => func(...args));
    }
  }
});
