import * as signalR from '@microsoft/signalr'

export class SignalRService {
  public hubUrl: string;
  public ready: boolean;
  public connectionId!: string;
  public hubConnection!: signalR.HubConnection

  constructor(hubUrl: string) {
    this.hubUrl = hubUrl;
    this.ready = false;
  }

  public startConnection = async () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`http://localhost:5000/hubs${this.hubUrl}`)
      .build();
    await this.hubConnection
      .start()
      .then(console.log)
      .then(() => this.getConnectionId())
      .catch(console.log)
  }

  public getConnectionId = () => {
    this.hubConnection.invoke('getconnectionid').then(
      (data) => {
          this.connectionId = data;
          this.ready = true;
        }
    ); 
  }
}