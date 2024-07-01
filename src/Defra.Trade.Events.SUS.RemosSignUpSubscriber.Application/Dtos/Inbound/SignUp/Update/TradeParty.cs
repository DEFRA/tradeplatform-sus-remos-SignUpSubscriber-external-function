// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Dtos.Inbound.SignUp.Update
{
    public sealed class TradeParty
    {
        public AuthorisedSignatory? AuthorisedSignatory { get; set; }
        public string? Id { get; set; }
        public string? OrgId { get; set; }
        public string? SignUpRequestSubmittedBy { get; set; }
        public TradeContactUpdate? TradeContact { get; set; }
    }
}