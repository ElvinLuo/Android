package com.expedia.lux.accountsettingstest.common.services.serviceports.csharp.LodgingInventoryPropertyService;

import com.expedia.lux.accountsettingstest.common.services.serviceports.csharp.LodgingInventoryPropertyService.CurrencyThreshold.CurrencyThresholdGetV1.CurrencyThresholdGetV1;
import com.expedia.lux.accountsettingstest.common.services.serviceports.csharp.LodgingInventoryPropertyService.RatePlanLinkage.RatePlanLinkageListV1.RatePlanLinkageListV1;

/**
 * Created by elluo on 11/27/2014.
 */
public class LodgingInventoryPropertyService {

    public static final int Port = 8090;
    public static final String CRSID = "LodgingInventoryPropertyService";
    public static final String BaseUriString = String.format("http://%s:%s/", "chelliappqa301", Port);

    public static CurrencyThresholdGetV1 CurrencyThresholdGetV1 = new CurrencyThresholdGetV1();
    public static RatePlanLinkageListV1 RatePlanLinkageListV1 = new RatePlanLinkageListV1();

}
