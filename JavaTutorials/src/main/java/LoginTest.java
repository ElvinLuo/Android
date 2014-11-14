import org.databene.benerator.anno.Source;
import org.databene.feed4junit.Feeder;
import org.junit.Test;
import org.junit.runner.RunWith;

/**
 * Created by elluo on 11/13/2014.
 */

@RunWith(Feeder.class)
public class LoginTest {

//    @Test
//    public void testLogin(String name, String password) {
//        System.out.println("name:" + name + " password:" + password);
//    }

    @Test
    @Source("data/userlogin.csv")
    public void testLoginCSV(String name, String password) {
        System.out.println("name:" + name + " password:" + password);
    }

    @Test
    @Source(uri = "data/values.xls", emptyMarker = "<empty>")
    public void testMethodSource(String name, String password) {
    	 System.out.println("name:" + name + " password:" + password);
    }

}