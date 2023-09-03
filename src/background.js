'use strict';

import { ipcMain, app, protocol, BrowserWindow } from 'electron';
import { createProtocol } from 'vue-cli-plugin-electron-builder/lib';
import installExtension, { VUEJS3_DEVTOOLS } from 'electron-devtools-installer';
import path from 'path';
import dotenv from 'dotenv';
import './server.js';
// import { stopServer } from './server.js';
dotenv.config({ path: path.join(__dirname, '../.env') });


const isDevelopment = process.env.NODE_ENV !== 'production';
const gamePort = process.env.COURSE_PORT || 8081; 

protocol.registerSchemesAsPrivileged([
  { scheme: 'app', privileges: { secure: true, standard: true } },
]);

async function createWindow() {
  const win = new BrowserWindow({
    width: 1400,
    height: 900,
    fullscreenable: true,
    resizable: true,
    movable: true,
    closable: true,
    minimizable: true,
    maximizable: true,
    webPreferences: {
      nodeIntegration: false,
      contextIsolation: true,
      enableRemoteModule: true,
      preload: path.join(__dirname, 'preload.js'),
    },
  });

  if (process.env.WEBPACK_DEV_SERVER_URL) {
    await win.loadURL(process.env.WEBPACK_DEV_SERVER_URL);
    if (!process.env.IS_TEST) win.webContents.openDevTools();
  } else {
    createProtocol('app');
    win.loadURL('app://./index.html');
  }
}

let unityWindow;

ipcMain.on('open-unity-window', (event, title) => {
  try {
    console.log('Received open-unity-window with title:', title);
    let courseURL = `http://localhost:${gamePort}/${encodeURIComponent(title)}`;
    unityWindow = new BrowserWindow({
      fullscreen: true,
      webPreferences: {
        nodeIntegration: false,
        contextIsolation: true,
        enableRemoteModule: false,
        preload: path.join(__dirname, 'preload.js'),
      },
    });

    unityWindow.loadURL(courseURL);
    console.log('Loaded course URL:', courseURL);
    unityWindow.on('closed', () => {
      unityWindow = null;
    });
  } catch (error) {
    console.log(error);
  }
});

ipcMain.on('close-unity-window', () => {
  try {
    if (unityWindow) {
      unityWindow.close();
      unityWindow = null;
    }
  } catch (error) {
    console.log(error);
  }
});

app.on('window-all-closed', () => {
  if (process.platform !== 'darwin') {
    app.quit();
  }
});

app.on('activate', () => {
  if (BrowserWindow.getAllWindows().length === 0) createWindow();
});

app.on('ready', async () => {
  if (isDevelopment && !process.env.IS_TEST) {
    try {
      await installExtension(VUEJS3_DEVTOOLS);
    } catch (e) {
      console.error('Vue Devtools failed to install:', e.toString());
    }
  }
  createWindow();
});

if (isDevelopment) {
  if (process.platform === 'win32') {
    process.on('message', (data) => {
      if (data === 'graceful-exit') {
        app.quit();
      }
    });
  } else {
    process.on('SIGTERM', () => {
      app.quit();
    });
  }
}
