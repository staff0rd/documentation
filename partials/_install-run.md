## Install and run Event Store

> [!NOTE]
> Unless you pass a database option with `--db`, Event Store writes to a new database created in the host system's temporary files path each time it is started. For more information on Command Line Arguments read [this guide](~/server/command-line-arguments.md).

### [Windows](#tab/tabid-1)

The prerequisites for installing on Windows are:

-   NET Framework 4.0+

Event Store has [Chocolatey packages](https://chocolatey.org/packages/eventstore-oss) available that you can install with the following command:

```powershell
choco install eventstore-oss
```

You can also [download](https://eventstore.org/downloads/) a binary, unzip the archive and run from the folder location with an administrator console.

The following command starts Event Store with the database stored at the path _./db_ and the logs in _./logs_. You can view further command line arguments in the [server docs](~/server/index.md).

```powershell
EventStore.ClusterNode.exe --db ./db --log ./logs
```

Event Store runs in an administration context because it starts an HTTP server through `http.sys`. For permanent or production instances you need to provide an ACL such as:

```powershell
netsh http add urlacl url=http://+:2113/ user=DOMAIN\username
```

For more information, refer to Microsoft's `add urlacl` [documentation](https://docs.microsoft.com/en-us/windows/win32/http/add-urlacl).

To build Event Store from source, refer to the [Event Store README](https://github.com/EventStore/EventStore#windows).

### [Linux](#tab/tabid-2)

The prerequisites for installing on Linux are:

-   We recommend [Mono 5.16.0](https://www.mono-project.com/download/stable/), but other versions may also work.

Event Store has pre-built [packages available for Debian-based distributions](https://packagecloud.io/EventStore/EventStore-OSS), [manual instructions for distributions that use RPM](https://packagecloud.io/EventStore/EventStore-OSS/install#bash-rpm), or you can [build from source](https://github.com/EventStore/EventStore#linux). The final package name to install is `eventstore-oss`.

If you installed from a pre-built package, start Event Store with:

```bash
sudo systemctl start eventstore
```

When you install the Event Store package, the service doesn't start by default. This is to allow you to change the configuration, located at _/etc/eventstore/eventstore.conf_ and to prevent creating a default database.

>[!TIP]
> To start Event Store on port 80 as a service, refer to [Configuring your installation](~/server/configuring.md#start-as-a-service-on-a-custom-port)

In all other cases you can run the Event Store binary or use our _run-node.sh_ shell script which exports the environment variable `LD_LIBRARY_PATH` to include the installation path of Event Store, which is necessary if you are planning to use projections.

```bash
./run-node.sh --db ./ESData
```

> [!NOTE]
> We recommend that when using Linux you set the 'open file limit' to a high number. The precise value depends on your use case, but at least between `30,000` and `60,000`.

### [Docker](#tab/tabid-3)

Event Store has a Docker image available for any platform that supports Docker.

Pull the Docker image:

```bash
docker pull eventstore/eventstore
```

Run the container:

```bash
docker run --name eventstore-node -it -p 2113:2113 -p 1113:1113 eventstore/eventstore
```

Refer to the [image overview](https://hub.docker.com/r/eventstore/eventstore/) for more information.

### [Kubernetes](#tab/tabid-k8)

Event Store has [a Helm chart](https://github.com/EventStore/EventStore.Charts) available for installing Event Store on Kubernetes clusters:

```bash
helm repo add eventstore https://eventstore.github.io/EventStore.Charts
helm repo update
helm install -n eventstore eventstore/eventstore
```

> [!NOTE]
> Read [this guide](https://github.com/EventStore/EventStore.Charts/blob/master/stable/eventstore/README.md) for more details on how to use the helm chart.
> Read [this guide](~/server/deploy-kubernetes-aks.md) for more details on using Kubernetes with AKS and this guide for more details on using Kubernetes with GKE.

### [macOS](#tab/tabid-macos)

Event Store has a macOS package [you can download](https://eventstore.org/downloads/) and install, and we maintain a Homebrew Cask formula you can install:

```shell
brew cask install eventstore
```

In each case you can run Event Store with the `eventstore` command, and stop it with `Ctrl+c`. To use the default database location you need to use `sudo`, or you can change the location with the `--db` parameter.

To build Event Store from source, refer to the [Event Store README](https://github.com/EventStore/EventStore#mac-os-x).

* * *
