package com.expedia.lux.accountsettingstest.common.services.framework;

import org.apache.http.HttpResponse;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.client.utils.URIBuilder;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClientBuilder;
import org.joda.time.DateTime;

import java.io.IOException;

/**
 * Created by elluo on 11/27/2014.
 */
public class MessageFacade {

    public static MessageResult send(URIBuilder uriBuilder, HttpUriRequest request) throws IOException {
        DateTime startTime = new DateTime();
        CloseableHttpClient httpClient = HttpClientBuilder.create().build();
        HttpResponse response = httpClient.execute(request);
        DateTime endTime = new DateTime();
        return new MessageResult(uriBuilder, request, response, startTime, endTime);
    }
}
