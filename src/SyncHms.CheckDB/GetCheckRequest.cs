﻿namespace SyncHms.CheckDB;

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.1.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(WrapperName = "GetCheck", WrapperNamespace = "http://tempuri.org/", IsWrapped = true)]
public partial class GetCheckRequest
{

    [System.ServiceModel.MessageBodyMemberAttribute(Namespace = "http://tempuri.org/", Order = 0)]
    [System.Xml.Serialization.XmlElementAttribute(IsNullable = true)]
    public Request check;

    public GetCheckRequest()
    {
    }

    public GetCheckRequest(Request check)
    {
        this.check = check;
    }
}
