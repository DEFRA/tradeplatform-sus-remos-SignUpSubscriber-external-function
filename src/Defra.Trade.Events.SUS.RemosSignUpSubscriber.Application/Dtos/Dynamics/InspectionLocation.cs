// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Crm;
using Defra.Trade.Crm.Metadata;

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Dynamics;

[CrmEntity("trd_inspectionlocation")]
public sealed class InspectionLocation
{
    [CrmProperty("trd_addressline1")]
    public string? AddressLine1 { get; set; }

    [CrmProperty("trd_addressline2")]
    public string? AddressLine2 { get; set; }

    [CrmProperty("trd_city")]
    public string? City { get; set; }

    [CrmProperty("rms_contactemailaddress")]
    public string? ContactEmailAddress { get; set; }

    [CrmProperty("trd_county")]
    public string? Country { get; set; }

    [CrmKey]
    [CrmProperty("trd_inspectionlocationid")]
    public Guid Id { get; set; }

    [CrmReference("contact", "rms_lastsubmittedby")]
    [CrmProperty("_rms_lastsubmittedby_value")]
    public Guid? LastSubmittedBy { get; set; }

    [CrmProperty("trd_locationname")]
    public string? LocationName { get; set; }

    [CrmProperty("trd_locationtypecode")]
    public LocationType? LocationType { get; set; }

    [CrmReference("account", "rms_traderid")]
    [CrmProperty("_rms_traderid_value")]
    public Guid? OrganisationId { get; set; }

    [CrmProperty("trd_postcode")]
    public string? Postcode { get; set; }

    [CrmProperty("rms_remosid")]
    public string? RmsEstablishmentNumber { get; set; }

    [CrmProperty("statecode")]
    public int? StateCode { get; set; }

    [CrmProperty("statuscode")]
    public int? StatusCode { get; set; }
}