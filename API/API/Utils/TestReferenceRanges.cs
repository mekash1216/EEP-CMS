using Humanizer;
using System.Collections.Generic;
using Twilio.TwiML.Voice;

namespace API.Utils
{
    public static class TestReferenceRanges
    {
        // Numeric tests reference ranges with units and optional gender-specific ranges
        public static readonly List<(string Test, decimal? Min, decimal? Max, string Unit, string Gender, string Condition)> NumericReferenceRanges = new List<(string Test, decimal? Min, decimal? Max, string Unit, string Gender, string Condition)>
        {
            { ("WBC" ,  4,10 , "K/mm3" ,"null","Normal") },
            { ("rbs", 80, 140, "mg/dL", null, "Normal") },
            { ("fbs", 70, 120, "mg/dL", null, "Normal") },
            { ("bun", 10, 50, "mg/dL", null, "Normal") },
            { ("creatinine", 0.7m, 1.3m, "mg/dL", "M", "Normal") }, // Men
            { ("creatinine", 0.6m, 1.1m, "mg/dL", "F", "Normal") }, // Women
            { ("uricAcid", 3.4m, 7.0m, "mg/dL", "M", "Normal") }, // Male
            { ("uricAcid", 2.4m, 5.7m, "mg/dL", "F", "Normal") }, // Female
            { ("tProtein", 6.0m, 8.3m, "g/dL", null, "Normal") },
            { ("tBilirubin", 0.0m, 1.1m, "mg/dL", null, "Normal") },
            { ("dBilirubin", 0.0m, 0.25m, "mg/dL", null, "Normal") },
            { ("albumin", 3.5m, 5.0m, "g/dL", null, "Normal") },
            { ("sgot", 0, 37, "U/L", "M", "Normal") }, // Male
            { ("sgot", 0, 31, "U/L", "F", "Normal") }, // Female
            { ("sgpt", 0, 42, "U/L", "M", "Normal") }, // Male
            { ("sgpt", 0, 32, "U/L", "F", "Normal") }, // Female
            { ("alp", 80, 306, "U/L", "M", "Normal") }, // Men
            { ("alp", 64, 306, "U/L", "F", "Normal") }, // Women
            { ("tCholesterol", 0, 200, "mg/dL", null, "Normal") },
            { ("hdlc", 40, decimal.MaxValue, "mg/dL", "M", "Normal") }, // Men
            { ("hdlc", 50, decimal.MaxValue, "mg/dL", "F", "Normal") }, // Women
            { ("ldlc", 0, 130, "mg/dL", null, "Normal") },
            { ("triglyceride", 0, 200, "mg/dL", null, "Normal") },
            { ("amylase", 25, 125, "U/L", null, "Normal") },
            
            // New tests
            { ("CRP (Quantitative)", null, 3, "mg/L", null, "Normal") },
            { ("hepatitisCViralLoad", null, 800000, "IU/mL", null, "Low") },
            { ("hepatitisCViralLoad", 800000, null, "IU/mL", null, "High") },
            { ("hepatitisCViralLoad", null, 800000, "IU/mL", null, "Low") },
            { ("hepatitisCViralLoad", 800000, null, "IU/mL", null, "High") },

            // Electrolyte
            { ("sodium", 135, 145, "mmol/L", null, "Normal") },
            { ("potassium", 3.5m, 5.0m, "mmol/L", null, "Normal") },
            { ("chloride", 95, 105, "mmol/L", null, "Normal") },
            { ("calcium", 8.5m, 10.3m, "mmol/L", null, "Normal") },
            { ("magnesium", 0.7m, 1.0m, "mmol/L", null, "Normal") },
            { ("phosphorus", 2.5m, 4.0m, "mg/dL", null, "Normal") },

            // Cancer Marker
            { ("cA125", 0, 35, "U/mL", null, "Normal") },
            { ("cA199", 0, 37, "U/mL", null, "Normal") },
            { ("cA153", null, 30, "U/mL", null, "Normal") },
            { ("cea", 0, 2.5m, "ng/mL", null, "Normal") },
            { ("afp", 0, 10, "ng/mL", "A", "Normal") }, // Adult
            { ("afp", 10, 150, "ng/mL", "P", "Normal") }, // Pregnant Women

            // Cardiac Marker
            { ("ckmb", null, 5, "ng/mL", null, "Normal") },
            { ("troponinT", null, 14, "ng/mL", null, "Normal") },
            { ("dDimer", null, 500, "ng/mL", null, "Normal") },

            // Coagulation
            { ("pt", 11, 13.5m, "seconds", null, "Normal") },
            { ("appt", 25, 35, "seconds", null, "Normal") },
            { ("inr", 0.9m, 1.2m, "", null, "Normal") },

            // Hormone Test
            { ("tsh", 0.5m, 5.0m, "mIU/L", null, "Normal") },
            { ("freeT4", 0.7m, 1.9m, "ng/dL", null, "Normal") },
            { ("freeT3", 2.3m, 4.2m, "pg/mL", null, "Normal") },
            { ("totalT4", 5.0m, 12.0m, "μg/dL", null, "Normal") },
            { ("totalT3", 80, 220, "ng/dL", null, "Normal") },
            { ("vitB12e", 200, 900, "pg/mL", null, "Normal") },
            { ("vitD", 43, 143, "pmol/L", null, "Normal") },

             // PSA
    { ("psa", 0, 2.5m, "ng/mL", null, "Age 40-49") }, // Age 40-49 years
    { ("psa", 0, 3.5m, "ng/mL", null, "Age 50-59") }, // Age 50-59 years
    { ("psa", 0, 4.5m, "ng/mL", null, "Age 60-69") }, // Age 60-69 years
    { ("psa", 0, 6.5m, "ng/mL", null, "Age 70-79") }, // Age 70-79 years
        };

        // Define the static readonly list
        public static readonly List<(string Test, decimal? Min, decimal? Max, string Unit, string Gender, string Phase)> BetaHCGReferenceRanges = new List<(string, decimal?, decimal?, string, string, string)>
    {
            {  ("totalBetaHCGT3", null, 5, "mIU/mL", "M", "Normal") },
       { ("totalBetaHCGT3", null, 5, "mIU/mL", "F", "Non-pregnant") },
       { ("totalBetaHCGT3", 5, 50, "mIU/mL", "F", "Early pregnancy (3-4 weeks from LMP)") },
        {("totalBetaHCGT3", 5, 426, "mIU/mL", "F", "Early pregnancy (4-5 weeks from LMP)") },
        {("totalBetaHCGT3", 19, 740, "mIU/mL", "F", "Early pregnancy (5-6 weeks from LMP)") },
        
         // FSH
    { ("fsh", 1.5m, 12.4m, "mIU/mL", "M", "Normal") }, // Men
    { ("fsh", 3.5m, 12.5m, "mIU/mL", "F", "Follicular phase") },
    { ("fsh", 4.7m, 21.5m, "mIU/mL", "F", "Midcycle peak") },
    { ("fsh", 1.7m, 7.7m, "mIU/mL", "F", "Luteal phase") },
    { ("fsh", 25.8m, 134.8m, "mIU/mL", "F", "Postmenopausal") },
    
    // LH
    { ("lh", 1.8m, 8.6m, "mIU/mL", "M", "Normal") }, // Men
    { ("lh", 1.9m, 12.5m, "mIU/mL", "F", "Follicular phase") },
    { ("lh", 21.9m, 56.6m, "mIU/mL", "F", "Midcycle peak") },
    { ("lh", 0.5m, 16.9m, "mIU/mL", "F", "Luteal phase") },
    { ("lh", 15.9m, 54.0m, "mIU/mL", "F", "Postmenopausal") },

          // Prolactin
          { ("prolactin", null, 20, "mU/L", "M", "Normal") },
          { ("prolactin", 5, 40, "mU/L", "F", "Non-Pregnant Females") },
          { ("prolactin", 30, 400, "mU/L", "F", "Pregnancy") },
           { ("prolactin", 36, 213, "mU/L", "F", "First trimester") },
            { ("prolactin", 110, 330, "mU/L", "F", "Second trimester") },
             { ("prolactin", 137, 372, "mU/L", "F", "Third trimester") },

           // GGT
           { ("ggt", null, 100, "mg/dL",  "F", "Fasting plasma glucose") },
        { ("ggt", null, 140, "mg/dL",  "F", "2-hour plasma glucose (after 75g glucose load)") },
        { ("ggt", 140, 199, "mg/dL",  "F", "Normal") },
        { ("ggt", 200, null, "mg/dL",  "F", "Impaired glucose tolerance") },
        { ("ggt", null, 100, "mg/dL", "M", "Fasting plasma glucose") },
        { ("ggt", null, 140, "mg/dL", "M", "2-hour plasma glucose (after 75g glucose load)") },
        { ("ggt", 140, 199, "mg/dL", "M", "Normal") },
        { ("ggt", 200, null, "mg/dL", "M", "Impaired glucose tolerance") },

         // LDH
       { ("ldh", 140, 280, "U/L", "M", "Serum") },
       { ("ldh", null, 40, "U/L", "M", "CSF-Adult") },
       { ("ldh", null, 70, "U/L", "M", "CSF-NewBorn") },
       { ("ldh", 140, 280, "U/L", "F", "Serum") },
       { ("ldh", null, 40, "U/L", "F", "CSF-Adult") },
       { ("ldh", null, 70, "U/L", "F", "CSF-NewBorn") }

    };

   }


    public static class TestSubReferenceRanges
    {
        // Numeric tests reference ranges with units and optional gender-specific ranges
        public static readonly List<(string Test, decimal? Min, decimal? Max, string Unit, string Gender, string Condition)> SubNumericReferenceRanges = new List<(string Test, decimal? Min, decimal? Max, string Unit, string Gender, string Condition)>
        {
            // WBC and related tests
            ("WBC", 4, 10, "K/mm3", null, "Normal"),
            ("Neu", 2, 7, "K/mm3", null, "Normal"),
            ("Lym", 0.8m, 4, "K/mm3", null, "Normal"),
            ("Mon", 0.12m, 1.2m, "K/mm3", null, "Normal"),
            ("Eos", 0.02m, 0.5m, "K/mm3", null, "Normal"),
            ("Bas", 0, 0.1m, "K/mm3", null, "Normal"),
            ("Neu", 50, 70, "%", null, "Normal"),
            ("Lym", 20, 40, "%", null, "Normal"),
            ("Mon", 3, 12, "%", null, "Normal"),
            ("Eos", 0.5m, 5, "%", null, "Normal"),
            ("Bas", 0, 0.1m, "%", null, "Normal"),

            // RBC and related tests
            ("RBC", 4.5m, 6.1m, "Million cells/UL", "M", "Normal"), // Males
            ("RBC", 4.0m, 5.4m, "Million cells/UL", "F", "Normal"), // Females
            ("HGB", 13.0m, 17.0m, "g/dL", "M", "Normal"), // Males
            ("HGB", 11.5m, 15.5m, "g/dL", "F", "Normal"), // Females
            ("HCT", 40, 55, "%", "M", "Normal"), // Males
            ("HCT", 36, 48, "%", "F", "Normal"), // Females
            ("MCV", 80, 100, "fL", null, "Normal"),
            ("MCH", 27, 34, "pg", null, "Normal"),
            ("MCHC", 32, 36, "g/dL", null, "Normal"),
            ("RDW-CV", 11, 16, "%", null, "Normal"),
            ("RDW-SD", 35, 56, "fL", null, "Normal"),

            // Platelet-related tests
            ("PLT", 100, 450, "K/mm3", null, "Normal"),
            ("MPV", 7, 11, "fL", null, "Normal"),
            ("PDW", 9, 17, "", null, "Normal"),
            ("PCT", 1.08m, 2.82m, "ML/L", null, "Normal"),
            ("p-LCC", 30, 90, "K/mm3", null, "Normal"),
            ("P-LCR", 0.11m, 0.45m, "", null, "Normal"),

            // Example of existing tests (partial)
            ("rbs", 80, 140, "mg/dL", null, "Normal"),
            ("fbs", 70, 120, "mg/dL", null, "Normal"),
            ("bun", 10, 50, "mg/dL", null, "Normal"),

            // PSA with age conditions
            ("psa", 0, 2.5m, "ng/mL", null, "Age 40-49"),
            ("psa", 0, 3.5m, "ng/mL", null, "Age 50-59"),
            ("psa", 0, 4.5m, "ng/mL", null, "Age 60-69"),
            ("psa", 0, 6.5m, "ng/mL", null, "Age 70-79"),

            // More tests (you can continue adding)
            ("HGB", 13.0m, 17.0m, "g/dL", "M", "Normal"), // Males
            ("HGB", 11.5m, 15.5m, "g/dL", "F", "Normal"), // Females
            // ...other tests
        };
    }
}
