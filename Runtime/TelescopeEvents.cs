﻿
/*
* --- Telescope Labs Auto Generated Code ---
* Date:Fri, 20 Jan 2023 07:06:58 GMT
* Please do not edit this code. Otherwise sdk may not work as expected.
*/

using System.Linq;
using System;
using System.Collections.Generic;

namespace telescope
{
    public static partial class Telescope
    {
        public static void AdImpressions(string abtestvalues = null, string ad_network = null, string ad_placement = null, string ad_source = null, string ad_unit = null, string appversion = null, int? appversionid = null, int? cheater = null, string country = null, DateTime? created = null, string device = null, int? deviceid = null, int? eventlevel = null, string firstappversion = null, int? firstappversionid = null, DateTime? firstpaymentdate = null, DateTime? lastpaymentdate = null, int? level = null, string locale = null, string location = null, int? locationid = null, string osversion = null, int? osversionid = null, int? paymentcount = null, double? paymentsum = null, double? revenue = null, DateTime? sbsfirstpaymentdate = null, int? sbspaymentcount = null, double? sbspaymentsum = null, string sdkversion = null, int? sdkversionid = null, string segmentvalues = null, int? tester = null)
        {
            var values = new Dictionary<string, object>() {
                { "$tl_schema_version", 1},
                { "abtestvalues", abtestvalues },
                { "ad_network", ad_network },
                { "ad_placement", ad_placement },
                { "ad_source", ad_source },
                { "ad_unit", ad_unit },
                { "appversion", appversion },
                { "appversionid", appversionid },
                { "cheater", cheater },
                { "country", country },
                { "created", created },
                { "device", device },
                { "deviceid", deviceid },
                { "eventlevel", eventlevel },
                { "firstappversion", firstappversion },
                { "firstappversionid", firstappversionid },
                { "firstpaymentdate", firstpaymentdate },
                { "lastpaymentdate", lastpaymentdate },
                { "level", level },
                { "locale", locale },
                { "location", location },
                { "locationid", locationid },
                { "osversion", osversion },
                { "osversionid", osversionid },
                { "paymentcount", paymentcount },
                { "paymentsum", paymentsum },
                { "revenue", revenue },
                { "sbsfirstpaymentdate", sbsfirstpaymentdate },
                { "sbspaymentcount", sbspaymentcount },
                { "sbspaymentsum", sbspaymentsum },
                { "sdkversion", sdkversion },
                { "sdkversionid", sdkversionid },
                { "segmentvalues", segmentvalues },
                { "tester", tester },
            }.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value);
            Track(new TelescopeGenericTrack("$tl_ad_impressions", values));
        }

        public static void CustomOffChainEvents(int cheater, string eventName, int tester, int? customBooleanProperty1 = null, double? customNumberProperty1 = null, double? customNumberProperty2 = null, string customStringProperty1 = null, string customStringProperty2 = null, string customStringProperty3 = null)
        {
            var values = new Dictionary<string, object>() {
                { "$tl_schema_version", 1},
                { "cheater", cheater },
                { "customBooleanProperty1", customBooleanProperty1 },
                { "customNumberProperty1", customNumberProperty1 },
                { "customNumberProperty2", customNumberProperty2 },
                { "customStringProperty1", customStringProperty1 },
                { "customStringProperty2", customStringProperty2 },
                { "customStringProperty3", customStringProperty3 },
                { "eventName", eventName },
                { "tester", tester },
            }.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value);
            Track(new TelescopeGenericTrack("$tl_custom_events", values));
        }

        public static void IngamePurchases(int? amount = null, int? appversionid = null, int? cheater = null, string country = null, string currency = null, int? deviceid = null, int? firstappversionid = null, string item = null, string itemtype = null, int? level = null, string locale = null, int? osversionid = null, int? p1 = null, int? paying_status = null, int? paymentcount = null, int? paymentsum = null, double? price = null, int? sbspaymentcount = null, int? sbspaymentsum = null, int? tester = null)
        {
            var values = new Dictionary<string, object>() {
                { "$tl_schema_version", 1},
                { "amount", amount },
                { "appversionid", appversionid },
                { "cheater", cheater },
                { "country", country },
                { "currency", currency },
                { "deviceid", deviceid },
                { "firstappversionid", firstappversionid },
                { "item", item },
                { "itemtype", itemtype },
                { "level", level },
                { "locale", locale },
                { "osversionid", osversionid },
                { "p1", p1 },
                { "paying_status", paying_status },
                { "paymentcount", paymentcount },
                { "paymentsum", paymentsum },
                { "price", price },
                { "sbspaymentcount", sbspaymentcount },
                { "sbspaymentsum", sbspaymentsum },
                { "tester", tester },
            }.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value);
            Track(new TelescopeGenericTrack("$tl_ingame_purchases", values));
        }

        public static void LevelUps(int? appversionid = null, int? cheater = null, string country = null, int? deviceid = null, int? eventlevel = null, int? firstappversionid = null, int? level = null, string locale = null, int? osversionid = null, int? paying_status = null, int? paymentcount = null, int? paymentsum = null, int? sbspaymentcount = null, int? sbspaymentsum = null, int? tester = null)
        {
            var values = new Dictionary<string, object>() {
                { "$tl_schema_version", 1},
                { "appversionid", appversionid },
                { "cheater", cheater },
                { "country", country },
                { "deviceid", deviceid },
                { "eventlevel", eventlevel },
                { "firstappversionid", firstappversionid },
                { "level", level },
                { "locale", locale },
                { "osversionid", osversionid },
                { "paying_status", paying_status },
                { "paymentcount", paymentcount },
                { "paymentsum", paymentsum },
                { "sbspaymentcount", sbspaymentcount },
                { "sbspaymentsum", sbspaymentsum },
                { "tester", tester },
            }.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value);
            Track(new TelescopeGenericTrack("$tl_levelups", values));
        }

        public static void LevelUpsCurrencies(int? appversionid = null, int? cheater = null, string country = null, string currencytype = null, int? deviceid = null, int? firstappversionid = null, int? level = null, string locale = null, int? osversionid = null, int? p1 = null, int? paying_status = null, int? paymentcount = null, int? paymentsum = null, int? sbspaymentcount = null, int? sbspaymentsum = null, int? tester = null)
        {
            var values = new Dictionary<string, object>() {
                { "$tl_schema_version", 1},
                { "appversionid", appversionid },
                { "cheater", cheater },
                { "country", country },
                { "currencytype", currencytype },
                { "deviceid", deviceid },
                { "firstappversionid", firstappversionid },
                { "level", level },
                { "locale", locale },
                { "osversionid", osversionid },
                { "p1", p1 },
                { "paying_status", paying_status },
                { "paymentcount", paymentcount },
                { "paymentsum", paymentsum },
                { "sbspaymentcount", sbspaymentcount },
                { "sbspaymentsum", sbspaymentsum },
                { "tester", tester },
            }.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value);
            Track(new TelescopeGenericTrack("$tl_levelups_currencies", values));
        }

        public static void Payments(string paymentid, int? appversionid = null, int? cheater = null, string country = null, string currency = null, int? deviceid = null, int? firstappversionid = null, int? level = null, string locale = null, int? osversionid = null, int? paymentcount = null, int? paymentsum = null, double? price = null, double? priceusd = null, int? product = null, int? sbspaymentcount = null, int? sbspaymentsum = null, int? tester = null, int? valid = null)
        {
            var values = new Dictionary<string, object>() {
                { "$tl_schema_version", 1},
                { "appversionid", appversionid },
                { "cheater", cheater },
                { "country", country },
                { "currency", currency },
                { "deviceid", deviceid },
                { "firstappversionid", firstappversionid },
                { "level", level },
                { "locale", locale },
                { "osversionid", osversionid },
                { "paymentcount", paymentcount },
                { "paymentid", paymentid },
                { "paymentsum", paymentsum },
                { "price", price },
                { "priceusd", priceusd },
                { "product", product },
                { "sbspaymentcount", sbspaymentcount },
                { "sbspaymentsum", sbspaymentsum },
                { "tester", tester },
                { "valid", valid },
            }.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value);
            Track(new TelescopeGenericTrack("$tl_payments", values));
        }

        public static void Progressions(int? appversionid = null, int? cheater = null, string country = null, int? currencytype = null, int? deviceid = null, int? difficalty = null, int? duration = null, int? firstappversionid = null, int? level = null, string locale = null, int? locationid = null, int? locationsource = null, int? osversionid = null, int? paying_status = null, int? paymentcount = null, int? paymentsum = null, int? sbspaymentcount = null, int? sbspaymentsum = null, int? success = null, int? tester = null)
        {
            var values = new Dictionary<string, object>() {
                { "$tl_schema_version", 1},
                { "appversionid", appversionid },
                { "cheater", cheater },
                { "country", country },
                { "currencytype", currencytype },
                { "deviceid", deviceid },
                { "difficalty", difficalty },
                { "duration", duration },
                { "firstappversionid", firstappversionid },
                { "level", level },
                { "locale", locale },
                { "locationid", locationid },
                { "locationsource", locationsource },
                { "osversionid", osversionid },
                { "paying_status", paying_status },
                { "paymentcount", paymentcount },
                { "paymentsum", paymentsum },
                { "sbspaymentcount", sbspaymentcount },
                { "sbspaymentsum", sbspaymentsum },
                { "success", success },
                { "tester", tester },
            }.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value);
            Track(new TelescopeGenericTrack("$tl_progressions", values));
        }

        public static void PushClicked(int? appversionid = null, int? cheater = null, string country = null, int? firstappversionid = null, int? level = null, string locale = null, int? osversionid = null, int? paying_status = null, int? paymentcount = null, int? paymentsum = null, int? sbspaymentcount = null, int? sbspaymentsum = null, int? tag_id = null, int? tester = null)
        {
            var values = new Dictionary<string, object>() {
                { "$tl_schema_version", 1},
                { "appversionid", appversionid },
                { "cheater", cheater },
                { "country", country },
                { "firstappversionid", firstappversionid },
                { "level", level },
                { "locale", locale },
                { "osversionid", osversionid },
                { "paying_status", paying_status },
                { "paymentcount", paymentcount },
                { "paymentsum", paymentsum },
                { "sbspaymentcount", sbspaymentcount },
                { "sbspaymentsum", sbspaymentsum },
                { "tag_id", tag_id },
                { "tester", tester },
            }.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value);
            Track(new TelescopeGenericTrack("$tl_push_clicked", values));
        }

        public static void PushSent(int? appversionid = null, int? cheater = null, string country = null, int? deviceid = null, int? firstappversionid = null, int? level = null, string locale = null, int? osversionid = null, int? paying_status = null, int? paymentcount = null, int? paymentsum = null, int? sbspaymentcount = null, int? sbspaymentsum = null, int? tag_id = null, int? tester = null)
        {
            var values = new Dictionary<string, object>() {
                { "$tl_schema_version", 1},
                { "appversionid", appversionid },
                { "cheater", cheater },
                { "country", country },
                { "deviceid", deviceid },
                { "firstappversionid", firstappversionid },
                { "level", level },
                { "locale", locale },
                { "osversionid", osversionid },
                { "paying_status", paying_status },
                { "paymentcount", paymentcount },
                { "paymentsum", paymentsum },
                { "sbspaymentcount", sbspaymentcount },
                { "sbspaymentsum", sbspaymentsum },
                { "tag_id", tag_id },
                { "tester", tester },
            }.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value);
            Track(new TelescopeGenericTrack("$tl_push_sent", values));
        }

        public static void Subscriptions(int? appversionid = null, int? cheater = null, string country = null, string currency = null, int? deviceid = null, int? firstappversionid = null, int? level = null, string locale = null, int? osversionid = null, int? p21 = null, int? paymentcount = null, string paymentid = null, int? paymentsum = null, string period = null, double? price = null, double? priceusd = null, int? product = null, string sbs_state = null, int? sbspaymentcount = null, int? sbspaymentsum = null, int? tester = null, int? valid = null)
        {
            var values = new Dictionary<string, object>() {
                { "$tl_schema_version", 1},
                { "appversionid", appversionid },
                { "cheater", cheater },
                { "country", country },
                { "currency", currency },
                { "deviceid", deviceid },
                { "firstappversionid", firstappversionid },
                { "level", level },
                { "locale", locale },
                { "osversionid", osversionid },
                { "p21", p21 },
                { "paymentcount", paymentcount },
                { "paymentid", paymentid },
                { "paymentsum", paymentsum },
                { "period", period },
                { "price", price },
                { "priceusd", priceusd },
                { "product", product },
                { "sbs_state", sbs_state },
                { "sbspaymentcount", sbspaymentcount },
                { "sbspaymentsum", sbspaymentsum },
                { "tester", tester },
                { "valid", valid },
            }.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value);
            Track(new TelescopeGenericTrack("$tl_subscriptions", values));
        }

        public static void Tutorials(int? appversionid = null, int? cheater = null, string country = null, int? deviceid = null, DateTime? eventtime_daily = null, int? firstappversionid = null, int? level = null, string locale = null, int? osversionid = null, int? paying_status = null, int? paymentcount = null, int? paymentsum = null, int? sbspaymentcount = null, int? sbspaymentsum = null, int? step = null, int? tester = null)
        {
            var values = new Dictionary<string, object>() {
                { "$tl_schema_version", 1},
                { "appversionid", appversionid },
                { "cheater", cheater },
                { "country", country },
                { "deviceid", deviceid },
                { "eventtime_daily", eventtime_daily },
                { "firstappversionid", firstappversionid },
                { "level", level },
                { "locale", locale },
                { "osversionid", osversionid },
                { "paying_status", paying_status },
                { "paymentcount", paymentcount },
                { "paymentsum", paymentsum },
                { "sbspaymentcount", sbspaymentcount },
                { "sbspaymentsum", sbspaymentsum },
                { "step", step },
                { "tester", tester },
            }.Where(x => x.Value != null).ToDictionary(x => x.Key, x => x.Value);
            Track(new TelescopeGenericTrack("$tl_tutorials", values));
        }


    }
}

/*

*/
