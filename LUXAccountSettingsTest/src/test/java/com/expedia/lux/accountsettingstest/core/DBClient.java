package com.expedia.lux.accountsettingstest.core;

import org.junit.Assert;

import java.sql.*;
import java.text.MessageFormat;
import java.util.ArrayList;
import java.util.List;

public class DBClient {
    private static String connectionStringAB = TestConfig.getConfigValue("com.expedia.lux.test.db.AB/url");
    private static String username = TestConfig.getConfigValue("com.expedia.lux.test.db.Lodging/username");
    private static String password = TestConfig.getConfigValue("com.expedia.lux.test.db.Lodging/password");

    /**
     * Connects to a database, returns the connection
     *
     * @param connectionString
     * @param userName
     * @param password
     * @return
     */
    protected static Connection getConnection(String connectionString, String userName, String password) {
        Connection connection = null;
        try {
            Class.forName("net.sourceforge.jtds.jdbc.Driver");
        } catch (ClassNotFoundException e) {
            System.out.println(e.getMessage());
        }

        try {
            connection = DriverManager.getConnection(connectionString, userName, password);
        } catch (SQLException e) {
            System.out.println(e.getMessage());
        }

        return connection;
    }

    public static List<String> getHotelsByExperimentID(String experimentID) throws SQLException {
        List<String> hotelList = new ArrayList<String>();
        Connection connection = null;

        try {
            connection = getConnection(connectionStringAB, username, password);
            String sql = MessageFormat.format("SELECT HotelID FROM HotelGroupHotel WHERE HotelGroupID = {0}"
                    , experimentID);
            PreparedStatement ps = connection.prepareStatement(sql);
            final ResultSet result = ps.executeQuery();

            while (result.next()) {
                hotelList.add(result.getString("HotelID"));
            }
        } catch (SQLException e) {
            Assert.fail(e.getMessage());
        } finally {
            if (null != connection) {
                connection.close();
            }
        }
        return hotelList;
    }

    public static boolean updateExperimentByHotelID(String hotelID, String experimentID, String type)
            throws SQLException {
        boolean isSuccess = false;
        Connection connection = null;

        String sql = null;
        try {
            connection = getConnection(connectionStringAB, username, password);

            switch (type.toUpperCase()) {
                case "INSERT": {
                    sql = MessageFormat.format("INSERT INTO HotelGroupHotel VALUES({0},{1})",
                            experimentID, hotelID);
                    break;
                }
                case "UPDATE": {
                    sql = MessageFormat.format("UPDATE HotelGroupHotel SET HotelGroupID = {0} WHERE HotelID = {1}",
                            experimentID, hotelID);
                    break;
                }
                case "DELETE": {
                    sql = MessageFormat.format("DELETE FROM HotelGroupHotel WHERE HotelGroupID = {0} AND HotelID = {1}",
                            experimentID, hotelID);
                    break;
                }
                default: {
                    System.out.println("Undefined type for sql statement.");
                    break;
                }
            }

            PreparedStatement ps = connection.prepareStatement(sql);
            final int result = ps.executeUpdate();

            if (1 == result) {
                isSuccess = true;
                System.out.println("HotelID " + hotelID + " has been added into Experiment " + experimentID + " !");
            }
        } catch (SQLException e) {
            Assert.fail(e.getMessage());
        } finally {
            if (null != connection) {
                connection.close();
            }
        }
        return isSuccess;
    }

    // exec Sproc
    public static List<String> getAllExperiments() throws SQLException {
        List<String> experiments = new ArrayList<String>();
        Connection connection = null;
        String sql = "SupplierPortal.dbo.ExperimentLatestGet";

        try {
            connection = getConnection(connectionStringAB, username, password);
            PreparedStatement ps = connection.prepareStatement(sql);
            final ResultSet result = ps.executeQuery();

            while (result.next()) {
                experiments.add(result.getString("ExperimentID"));
            }
        } catch (SQLException e) {
            Assert.fail(e.getMessage());
        } finally {
            if (null != connection) {
                connection.close();
            }
        }
        return experiments;
    }

    public static boolean addNewExperiment(String experimentID) throws SQLException {
        boolean isSuccess = false;
        Connection connection = null;
        String expName = "Experiment " + experimentID;
        String sqlExp = MessageFormat.format("SupplierPortal.dbo.ExperimentMrg {0}, ''" + expName +
                "'', ''19000101'', ''20990101'', 1", experimentID);
        String sqlGrp = MessageFormat.format("SupplierPortal.dbo.HotelGroupMrg {0}, ''" + expName +
                "'', 1", experimentID);
        String sqlAso = MessageFormat.format("SupplierPortal.dbo.ExperimentHotelGroupMrg {0}, ''{0}''",
                experimentID);

        try {
            connection = getConnection(connectionStringAB, username, password);
            PreparedStatement psExp = connection.prepareStatement(sqlExp);
            boolean isResultSet = psExp.execute();
            int count = psExp.getUpdateCount();

            if (!isResultSet && (-1 == count)) {
                PreparedStatement psGrp = connection.prepareStatement(sqlGrp);
                isResultSet = psGrp.execute();
                count = psGrp.getUpdateCount();

                if (!isResultSet && (-1 == count)) {
                    PreparedStatement psAso = connection.prepareStatement(sqlAso);
                    isResultSet = psAso.execute();
                    count = psAso.getUpdateCount();

                    if (!isResultSet && (-1 == count)) {
                        isSuccess = true;
                        System.out.println("Experiment " + experimentID + " has been created !");
                    }
                }
            }
        } catch (SQLException e) {
            Assert.fail(e.getMessage());
        } finally {
            if (null != connection) {
                connection.close();
            }
        }
        return isSuccess;
    }

    public static boolean updateExperiments(String hotelIDs, String experimentID)
            throws SQLException {
        boolean isSuccess = false;
        Connection connection = null;
        int num = 0;

        String sql = MessageFormat.format("SupplierPortal.dbo.HotelGroupHotelMrg {0}, ''{1}''",
                experimentID, hotelIDs);
        try {
            connection = getConnection(connectionStringAB, username, password);
            PreparedStatement ps = connection.prepareStatement(sql);
            final boolean isResultSet = ps.execute();
            int count = ps.getUpdateCount();

            for (int index = 0; index < hotelIDs.length(); index++) {
                if (hotelIDs.charAt(index) == ',') {
                    num++;
                }
            }

            if ((!isResultSet && (-1 == count)) || (isResultSet && (count == num + 1))) {
                isSuccess = true;
                System.out.println("Experiment " + experimentID + " has been reset !");
            }
        } catch (SQLException e) {
            Assert.fail(e.getMessage());
        } finally {
            if (null != connection) {
                connection.close();
            }
        }
        return isSuccess;
    }

    public static void cleanHistory() throws SQLException {
        Connection connection = null;

        String sqlExp = "DELETE FROM ExperimentHist WHERE UpdateBy = 'TravPortal'";
        String sqlGrp = "DELETE FROM HotelGroupHist WHERE UpdateBy = 'TravPortal'";
        String sql = "DELETE FROM HotelGroupHotelHist WHERE UpdateBy = 'TravPortal'";
        try {
            connection = getConnection(connectionStringAB, username, password);
            PreparedStatement ps = connection.prepareStatement(sqlExp);
            ps.executeUpdate();
            ps = connection.prepareStatement(sqlGrp);
            ps.executeUpdate();
            ps = connection.prepareStatement(sql);
            ps.executeUpdate();
        } catch (SQLException e) {
            Assert.fail(e.getMessage());
        } finally {
            if (null != connection) {
                connection.close();
            }
        }
    }
}
