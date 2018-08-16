# OSS Vs Commercial editions

Event Store ships in two editions and this guide outlines the differences between the two.

**TODO: Was made aware of this page - <https://developers.eventstore.org/tools/>, we should think how to merge and better connect the content, they are pre/post account.**

## Open Source

### High availability

When your Event Store application is under high demand, you can create a cluster of instances to spread load. With the open source edition you can only create a cluster consisting of database instances.

### Support

Our community provides support through an active [Google group](https://groups.google.com/forum/#!forum/event-store) and [GitHub issue queue](https://github.com/eventstore/eventstore/issues).

## Commercial

### Support

We offer [commercial and enterprise](https://eventstore.org/support/) level support plans tailored to suit your needs.

### Infrastructure scripts

If you are looking to host your Event Store cluster on AWS or Azure cloud hosting, we provide industry standard Packer and Terraform scripts to help your team get setup following our recommended best practices.

### High availability

When your Event Store application is under high demand, you can create a cluster of instances to spread load. With the commercial edition the cluster can also contain manager nodes that help your cluster run more efficiently and make it easier to use.

### Database and backup checks

The DbVerifier tool lets you verify that your database backups are complete and valid without needing to start a node to do so.

### GeoReplicas

For greater data security and redundancy, the GeoReplica plugin allows you to maintain a copy of your Event Store data in an extrenal location.

### Node administration CLI

Members of your team dealing with development, deployment and operations will love our command line interface for administering nodes that allows them to run similar commands to the web admin interface.

### Monitoring Plugins and Scripts

We provide cluster and node health monitoring plugins and scripts for popular platforms such as Nagios.

**TODO: Any others? I'd not heard of Nagios.**

### Command and Event Correlation Visualizer

This plugin for to the Event Store web admin UI provides a visual overview of commands and the events related to them.

### LDAP Authentication Plugin

Use Microsoft Active Directory as an Authentication Provider for Event Store, helping it integrate better into your existing infrastructure.

### Windows Manager

Allows you to run Event Store as a Windows service, and acts as a supervisor for an Event Store node running under Windows.