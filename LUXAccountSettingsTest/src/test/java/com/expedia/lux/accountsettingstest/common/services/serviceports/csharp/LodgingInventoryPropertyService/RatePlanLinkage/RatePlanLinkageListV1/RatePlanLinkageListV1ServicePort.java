package com.expedia.lux.accountsettingstest.common.services.serviceports.csharp.LodgingInventoryPropertyService.RatePlanLinkage.RatePlanLinkageListV1;

import com.expedia.lux.accountsettingstest.common.services.framework.MessageFacade;
import com.expedia.lux.accountsettingstest.common.services.framework.MessageResult;
import org.apache.http.client.methods.HttpUriRequest;

import java.io.IOException;

/**
 * Created by elluo on 11/27/2014.
 */
public class RatePlanLinkageListV1ServicePort {

    public static MessageResult send(int hotelId) throws IOException {
        HttpUriRequest request = RatePlanLinkageListV1RequestBuilder.buildRequest(hotelId);
        return MessageFacade.send(null, request);
    }

}
