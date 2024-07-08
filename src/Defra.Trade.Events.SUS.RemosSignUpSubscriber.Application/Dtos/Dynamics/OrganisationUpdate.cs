// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

using Defra.Trade.Crm;
using Defra.Trade.Crm.Metadata;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Dynamics;

[CrmEntity("account")]
public sealed class OrganisationUpdate
{
    [CrmProperty("rms_authsignatoryemail")]
    public string? AuthorisedSignatoryEmail { get; set; }

    [CrmProperty("rms_authsignatoryname")]
    public string? AuthorisedSignatoryName { get; set; }

    [CrmProperty("rms_authsignatoryposition")]
    public string? AuthorisedSignatoryPosition { get; set; }

    [CrmKey]
    [CrmProperty("accountid")]
    public Guid Id { get; set; }

    [CrmReference("contact", "rms_lastsubmittedby")]
    [CrmProperty("_rms_lastsubmittedby_value")]
    public Guid? LastSubmittedBy { get; set; }

    [CrmProperty("rms_rmscontactemail")]
    public string? RmsBusinessContactEmail { get; set; }

    [CrmProperty("rms_rmscontactname")]
    public string? RmsBusinessContactName { get; set; }

    [CrmProperty("rms_rmscontactposition")]
    public string? RmsBusinessContactPosition { get; set; }

    [CrmProperty("rms_rmscontacttelephone")]
    public string? RmsBusinessContactTelephone { get; set; }
}