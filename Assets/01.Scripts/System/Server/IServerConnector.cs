using System;

public interface IServerConnector {
    void OnRequest(params object[] args);
}
