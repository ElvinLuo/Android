package com.expedia.lux.accountsettingstest.common.services.serviceports.csharp.LodgingInventoryPropertyService.RatePlanLinkage.RatePlanLinkageListV1;

/**
 * Created by elluo on 11/27/2014.
 */
public class RatePlanLinkageListV1 {

    public final String URI = "lips/V1/hotels/%d/rateplanlinks";
    public final String TransactionType = "RLA1";

    public RatePlanLinkageListV1RequestBuilder MessageFactory = new RatePlanLinkageListV1RequestBuilder();
    public RatePlanLinkageListV1ServicePort ServicePort = new RatePlanLinkageListV1ServicePort();

}
