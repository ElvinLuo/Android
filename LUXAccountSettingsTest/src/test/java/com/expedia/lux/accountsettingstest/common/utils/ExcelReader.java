package com.expedia.lux.accountsettingstest.common.utils;

import java.util.ArrayList;
import java.util.Collection;

/**
 * Created by elluo on 11/13/2014.
 */
public class ExcelReader {

    private String fileName;
    private String sheetName;

    public ExcelReader(String fileName, String sheetName) {
        this.fileName = fileName;
        this.sheetName = sheetName;
    }

    public Collection<Object[]> ReadSheet() {
        Collection<Object[]> value = new ArrayList<>();

        ArrayList<String> row1 = new ArrayList<>();
        row1.add("Test case name");
        row1.add("Priority");
        row1.add("First name");
        value.add(new Object[]{row1});

        ArrayList<String> row2 = new ArrayList<>();
        row2.add("First name = HotelApple");
        row2.add("1");
        row2.add("HotelApple");
        value.add(new Object[]{row2});

        return value;
    }
}
