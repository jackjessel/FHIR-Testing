using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using CsvHelper;
using CsvHelper.Configuration;
using System.Data;
using Microsoft.VisualBasic;

class CsvOrganisationMap : ClassMap<CsvOrganisation>
{
    public CsvOrganisationMap()
    {
        Map(m => m.Code).Name("Code");
        Map(m => m.Name).Name("Name");
        Map(m => m.PrimaryRoleName).Name("Primary Role Name");
        Map(m => m.LastChangeDate).Name("LastChangeDate");
        Map(m => m.ContactTelephoneNumber).Name("Contact Telephone Number");
        Map(m => m.AddressLine1).Name("AddressLine1");
        Map(m => m.AddressLine2).Name("AddressLine2");
        Map(m => m.AddressLine3).Name("AddressLine3");
        Map(m => m.Town).Name("Town");
        Map(m => m.Postcode).Name("Postcode");
        Map(m => m.County).Name("County");
    }
}

class Program 
{
    static void Main()
    {
        //Csv input path
        string csvFilePath = @"C:\Users\jack.jessel\Desktop\Interop\csv-json-fhir-liquid\ODSData.csv";
        //
        string jsonFilePath = @"C:\Users\jack.jessel\Desktop\Interop\csv-json-fhir-liquid\input.json";

        //Read csv 
        using (var reader = new StreamReader(csvFilePath))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<CsvOrganisationMap>();
            var records = csv.GetRecords<CsvOrganisation>();
            var jsonData = new List<Dictionary<string, object>>();
            foreach(var i in records)
            {
                var recordDict = ConvertRowToJson(i);
                jsonData.Add(recordDict);

            } 
            //write json data to a jsonFilePath
            var jsonOutput = JsonSerializer.Serialize(jsonData);
            File.WriteAllText(jsonFilePath, jsonOutput);
        }

    }
    static Dictionary<string, object> ConvertRowToJson(CsvOrganisation record)
    {
        var jsonDict = new Dictionary<string, object>
        {
            {"Code", record.Code},
            {"Name", record.Name},
            {"PrimaryRoleName", record.PrimaryRoleName},
            {"LastChangeDate", record.LastChangeDate},
            {"ContactTelephoneNumber", record.ContactTelephoneNumber},
            {"AddressLine1", record.AddressLine1},
            {"AddressLine2", record.AddressLine2},
            {"AddressLine3", record.AddressLine3},
            {"Town", record.Town},
            {"County", record.County},
            {"Postcode", record.Postcode}
            //some more stuff
        };

        return jsonDict;

    }
}
public class CsvOrganisation
{
    public string Code {get; set;}
    public string Name {get; set;}
    public string PrimaryRoleName {get; set;}
    public string LastChangeDate {get; set;}
    public string ?ContactTelephoneNumber {get; set;}
    public string ?AddressLine1 {get; set;}
    public string ?AddressLine2 {get; set;}        
    public string ?AddressLine3 {get; set;}
    public string ?Town {get; set;}
    public string ?County {get; set;}
    public string ?Postcode {get; set;}
}