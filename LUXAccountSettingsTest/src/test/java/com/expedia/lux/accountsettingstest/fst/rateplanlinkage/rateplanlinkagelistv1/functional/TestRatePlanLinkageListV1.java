package com.expedia.lux.accountsettingstest.fst.rateplanlinkage.rateplanlinkagelistv1.functional;

import com.expedia.lux.accountsettingstest.common.services.framework.MessageResult;
import com.expedia.lux.accountsettingstest.common.services.serviceports.csharp.LodgingInventoryPropertyService.LodgingInventoryPropertyService;
import org.junit.Test;

import java.io.IOException;

/**
 * Created by elluo on 11/27/2014.
 */
public class TestRatePlanLinkageListV1 {

    @Test
    public void testRatePlanLinkageListMessage() throws IOException {
        MessageResult messageResult = LodgingInventoryPropertyService.RatePlanLinkageListV1.ServicePort.send(6152629);
    }
}
