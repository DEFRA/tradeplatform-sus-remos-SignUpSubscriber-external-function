// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government Licence v3.0.

namespace Defra.Trade.Events.SUS.RemosSignUpSubscriber.Application.Models;

public static class RemosSignUpServiceHeaderConstants
{
    public const string ContentType = "application/json";
    public const string PublisherId = "SuS";

    public static class SignUp
    {
        public static class Create
        {
            public const string SchemaVersion = "1";
            public const string Status = "Created";
            public const string Label = "sus.remos.signup";
        }

        public static class Update
        {
            public const string SchemaVersion = "2";
            public const string Status = "Complete";
            public const string Label = "sus.remos.update";
        }
    }

    public static class Establishment
    {
        public static class Create
        {
            public const string SchemaVersion = "3";
            public const string Status = "Created";
            public const string Label = "sus.remos.establishment.create";
        }

        public static class Update
        {
            public const string SchemaVersion = "4";
            public const string Status = "Completed";
            public const string Label = "sus.remos.establishment.update";
        }
    }
}