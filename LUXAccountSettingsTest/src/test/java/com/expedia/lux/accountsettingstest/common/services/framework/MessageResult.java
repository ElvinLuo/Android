package com.expedia.lux.accountsettingstest.common.services.framework;

import org.apache.http.HttpRequest;
import org.apache.http.HttpResponse;
import org.apache.http.client.methods.HttpUriRequest;
import org.apache.http.client.utils.URIBuilder;
import org.apache.http.util.EntityUtils;
import org.joda.time.DateTime;

import java.io.IOException;

/**
 * Created by elluo on 11/27/2014.
 */
public class MessageResult {

    public MessageResult(URIBuilder uriBuilder,
                         HttpUriRequest request,
                         HttpResponse response,
                         DateTime startTime,
                         DateTime endTime) throws IOException {
        this.uriBuilder = uriBuilder;
        this.request = request;
        this.response = response;
        this.responseEntityString = EntityUtils.toString(response.getEntity());
        this.startTime = startTime;
        this.endTime = endTime;
    }

    public URIBuilder uriBuilder;
    public HttpRequest request;
    public HttpResponse response;
    public String responseEntityString;
    public DateTime startTime;
    public DateTime endTime;

}
