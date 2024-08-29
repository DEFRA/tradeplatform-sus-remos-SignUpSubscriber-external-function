// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.SignUp.Create;

public sealed class TradeParty
{
    public int? ApprovalStatus { get; set; }
    public DateTimeOffset? AssuranceCommitmentSignedDate { get; set; }
    public AuthorisedSignatory? AuthorisedSignatory { get; set; }
    public string? CountryName { get; set; }
    public DateTimeOffset? CreatedDate { get; set; }
    public string? FboNumber { get; set; }
    public Guid? Id { get; set; }
    public DateTimeOffset? LastUpdateDate { get; set; }
    public IEnumerable<LogisticsLocation>? LogisticsLocations { get; set; }
    public Guid? OrgId { get; set; }
    public string? PhrNumber { get; set; }
    public bool? RegulationsConfirmed { get; set; }
    public string? RemosBusinessSchemeNumber { get; set; }
    public Guid? SignUpRequestSubmittedBy { get; set; }
    public DateTimeOffset? TermsAndConditionsSignedDate { get; set; }
    public TradeContactSignUp? TradeContact { get; set; }
}