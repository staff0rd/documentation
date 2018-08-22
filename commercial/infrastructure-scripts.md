---
outputFileName: index.html
commercial: true
---

# Infrastructure Quick Start Scripts

## Amazon Web Services

This serves as a guide to setup an Event Store Cluster using Packer and Terraform on AWS. As all setups can be different, run them with caution.

### Download

<p class="call-to-action">
<a href="/downloads/EventStoreHA_AWS_Terraform.zip" class="btn btn--primary">Terraform/Packer scripts for AWS</a>
</p>

[Download scripts](https://developers.eventstore.org/downloads/EventStoreHA_AWS_Terraform.zip)

### Prerequisites
 
- [Packer](https://www.packer.io/) is a tool for creating machine and container images for multiple platforms from a single source configuration.
  - You need to setup Packer for use with AWS, [find instructions here](https://www.packer.io/docs/builders/amazon.html). 
- [Terraform](https://www.terraform.io) provides common configuration to launch infrastructure, from physical and virtual servers to email and DNS providers. Once launched, Terraform safely and efficiently changes infrastructure as the configuration evolves.
  - You need to setup Terraform for use with AWS, [find instructions here](https://terraform.io/docs/providers/aws/index.html).
- A User in AWS.
- SSH keypair (Used to allow a user to ssh into the instances setup as part of the scripts).
- Event Store AWS Cluster Setup Scripts (Provided by Event Store) <!-- TODO: What are these? -->
- Package Cloud Token (Provided by Event Store) <!-- TODO: What are these? -->

### Setting up Event Store Amazon Machine Image (AMI)

Run the `packer` command using the supplied _packer/template.json_ file, substituting your values:

```bash
packer build \
    -var 'source_ami={AMI_ID}' \
    -var 'region={PREFERED_REGION}' \
    -var 'token={PACKAGE_CLOUD_TOKEN}' \
    template.json
```

### Setting up Event Store Infrastructure (VPC and NAT)

The NAT script repurposes the NAT as a bastion box for SSH access. You may want to adjust the security groups to not allow SSH from the bastion box in the cluster. The script sets up the minimal necessary VPC and NAT infrastructure to get up and running with a new account.

- Copy your SSH keypair to _/terraform/keys_. The script expects a _private.pem.pub_ file.
- Run the following command in the _/terraform/infrastructure_ directory, substituting with the necessary information to fit your setup.

```bash
terraform plan \
  -var 'region=us-east-1' \
  -var 'cidr=10.15.0.0/16' \
  -var 'private_subnets=10.15.160.0/19,10.15.192.0/19,10.15.224.0/19' \
  -var 'public_subnets=10.15.0.0/21,10.15.8.0/21,10.15.16.0/21' \
  -var 'availability_zones=us-east-1a,us-east-1b,us-east-1c' \
  -var 'nat_ami=ami-354cfd5e'
 
elb_address = internal-eventstore-elb-1320141779.us-east-1.elb.amazonaws.com
elb_name = eventstore-elb
elb_sg_id = sg-379d1148
private_subnets = subnet-2ee21b66,subnet-6f878234,subnet-40e3a325
public_subnets = subnet-19ef1651,subnet-a68782fd,subnet-93e3a3f6
vpc_id = vpc-b0416cd6
vpn_setup_command = ssh openvpnas@34.207.182.171
vpn_web_console = https://34.207.182.171/
```

### Setting up the Event Store HA Cluster (Nodes with Auto Scaling Group (https://aws.amazon.com/autoscaling/) )

The node configuration is dynamic in the sense that it uses the AWS infrastructure to determine the IP Addresses of the other nodes in the Auto Scaling Group and on startup generates the appropriate configuration.

Run the following command in the _/terraform/cluster_ directory, substituting with the necessary information from the previous terraform script as well as the required number of nodes and their instance types. (We recommend `m3.large`).

```bash
terraform plan \
  -var 'region=us-east-1' \
  -var 'cluster_name=eventstore-cluster' \
  -var 'vpc_id=vpc-b0416cd6' \
  -var 'node_instance_type=t2.micro' \
  -var 'backup_bucket_name=eventstore-backup-bucket-2' \
  -var 'number_of_instances=3' \
  -var 'private_subnets=subnet-2ee21b66,subnet-6f878234,subnet-40e3a325' \
  -var 'availability_zones=us-east-1a,us-east-1b,us-east-1c' \
  -var 'node_ami=ami-ff35c89f' \
  -var 'elb_sg_id=sg-379d1148' \
  -var 'elb_name=eventstore-elb'
```

### Setting up OpenVPN

The output from the infrastructure setup contains a `vpn_setup_command = ssh openvpnas@{IP_ADDRESS}` value. Run the `ssh openvpnas@{IP_ADDRESS}` command and openVPN prompts you to set a password by running the `passwd openvpn` command

With a password set, you can download the openVPN software by navigating to the VPN web console using the URL above also outputted above, `vpn_web_console = https://34.207.182.171/` and the credentials `openvpn/{PASSWORD_SET_ABOVE}.

Once you have downloaded the openVPN connect software, you should be able to open the web admin UI of any of the nodes in the cluster.

If you open the _cluster_ tab, you should see a setup like the following. (The image below shows a healthy 3 node cluster. 1 Master and 2 Slaves)

<!-- TODO: IMAGE? -->

### Testing the setup

You are able to test that the nodes are up by SSHing into the NAT box and issuing a curl request to one of nodes.

## Microsoft Azure

This serves as a guide to setup an Event Store Cluster using Packer and Terraform on Azure. As all setups can be different, run them with caution.

### Download

<p class="call-to-action">
<a href="/downloads/EventStoreHA_Azure_Terraform.zip" class="btn btn--primary">Terraform/Packer scripts for Azure</a>
</p>

### Prerequisites

- [Packer](https://www.packer.io/) is a tool for creating machine and container images for multiple platforms from a single source configuration.
  - You need to setup Packer for use with Azure, [find instructions here](https://www.packer.io/docs/builders/azure-setup.html). 
- [Terraform](https://www.terraform.io) provides common configuration to launch infrastructure, from physical and virtual servers to email and DNS providers. Once launched, Terraform safely and efficiently changes infrastructure as the configuration evolves.
  - You need to setup Terraform for use with Azure, [find instructions here](https://www.terraform.io/docs/providers/azurerm).
- A resource group for the cluster and node image to be created in
- A storage account in the above resource group
- Event Store Azure Cluster Setup Scripts (Provided by Event Store) <!-- TODO: What are these? -->
- Package Cloud Token (Provided by Event Store) <!-- TODO: What are these? -->

### Setting up the Event Store Virtual Hard Disk image (VHD)

Run the `packer` command using the supplied _packer/template.json_ file, substituting your values:

```bash
packer build \
    -var 'token={PACKAGECLOUD_TOKEN}'  \
    -var 'subscription_id={AZURE_SUBSCRIPTION_ID}'  \
    -var 'client_id={AZURE_CLIENT_ID}'  \
    -var 'client_secret={AZURE_CLIENT_SECRET}'  \
    -var 'tenant_id={AZURE_TENANT_ID}'  \
    -var 'resource_group_name={DESTINATION_RESOURCE_GROUP}'  \
    -var 'storage_account={DESTINATION_STORAGE_ACCOUNT}'  \
    template.json
```

This creates a VHD in the provided storage account. You need to copy the URI of the VHD for use in the next step.

### Setting up the Event Store HA Cluster (Nodes with Load Balancer)

The Terraform scripts set up a cluster of 3 nodes with a load balancer that routes traffic from the public ports (`1113` and `2113`) and ssh (`22`) to one of the nodes. You can see and edit the configuration of the nodes in the _scripts/configure.sh_ script.

The default network security group for the nodes opens them to the world. You should update the security rules to better fit your use case.

To set up the cluster, run the following command in the _/terraform/cluster_ directory, substituting with the necessary information to fit your setup.

```bash
$ terraform plan \
    -var 'subscription_id={AZURE_SUBSCRIPTION_ID}' \
    -var 'client_id={AZURE_CLIENT_ID}' \
    -var 'client_secret={AZURE_CLIENT_SECRET}' \
    -var 'tenant_id={AZURE_TENANT_ID}' \
    -var 'region={AZURE_REGION}' \
    -var 'vm_size={AZURE_MACHINE_SIZE}' \
    -var 'vm_username=esadmin' \
    -var 'vm_password=Changeit!' \
    -var 'template_vhd_uri={AZURE_VHD_TEMPLATE_URI}' \
    -var 'storage_account_path={DESTINATION_STORAGE_ACCOUNT}'
```

Apply the changes by running `terraform apply`.

### Testing the setup

You should now have 3 virtual machines and a load balancer created in the resource group in Azure. To test the setup, open the public IP of the load balancer, log in to the admin UI, and open the _cluster_ tab. You should see a setup similar to the following 3 node cluster, containing 1 Master and 2 Slaves.

<!-- TODO: IMAGE AGAIN. -->