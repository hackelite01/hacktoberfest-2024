import org.apache.pdfbox.pdmodel.PDDocument;
import org.apache.pdfbox.pdmodel.PDPage;
import org.apache.pdfbox.pdmodel.PDPageContentStream;
import org.apache.pdfbox.pdmodel.font.PDType1Font;

import java.io.File;
import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

public class wDB {
    // Ensure this path is accessible
    private static final String FilePath = "D:\\b.pdf";  
    PDDocument invc;
    Integer total = 0;
    String CustName;
    String CustPh;
    List<String> MenuItemNames = new ArrayList<String>();
    List<Integer> MenuItemPrices = new ArrayList<Integer>();
    List<Integer> MenuItemQuantities = new ArrayList<Integer>();

    public wDB() throws IOException {
        invc = new PDDocument();
        invc.addPage(new PDPage());
    }

    public void setCustomerInfo(String name, String phone) {
        this.CustName = name;
        this.CustPh = phone;
    }

    public void addMenuItem(String itemName, int price, int quantity) {
        MenuItemNames.add(itemName);
        MenuItemPrices.add(price);
        MenuItemQuantities.add(quantity);
        total += price * quantity;
    }

    public void writeInvoice() throws IOException {
        // Delete the file if it already exists to prevent access issues
        File file = new File(FilePath);
        if (file.exists()) {
            file.delete();
        }

        PDPageContentStream cs = new PDPageContentStream(invc, invc.getPage(0));

        // Invoice Title
        cs.beginText();
        cs.setFont(PDType1Font.HELVETICA_BOLD, 20);
        cs.newLineAtOffset(100, 750);
        cs.showText("Food Corner Invoice");
        cs.endText();

        // Customer Details
        cs.beginText();
        cs.setFont(PDType1Font.HELVETICA, 12);
        cs.newLineAtOffset(100, 720);
        cs.showText("Customer Name: " + CustName);
        cs.newLineAtOffset(0, -15);
        cs.showText("Phone Number: " + CustPh);
        cs.endText();

        // Table Headers
        cs.beginText();
        cs.setFont(PDType1Font.HELVETICA_BOLD, 12);
        cs.newLineAtOffset(100, 690);
        cs.showText("Menu Item");
        cs.newLineAtOffset(150, 0);
        cs.showText("Price");
        cs.newLineAtOffset(100, 0);
        cs.showText("Quantity");
        cs.newLineAtOffset(100, 0);
        cs.showText("Total");
        cs.endText();

        // Menu Items
        int lineOffset = 675;
        for (int i = 0; i < MenuItemNames.size(); i++) {
            cs.beginText();
            cs.setFont(PDType1Font.HELVETICA, 12);
            cs.newLineAtOffset(100, lineOffset - (i * 15));
            cs.showText(MenuItemNames.get(i));
            cs.endText();

            cs.beginText();
            cs.setFont(PDType1Font.HELVETICA, 12);
            cs.newLineAtOffset(250, lineOffset - (i * 15));
            cs.showText(MenuItemPrices.get(i).toString());
            cs.endText();

            cs.beginText();
            cs.setFont(PDType1Font.HELVETICA, 12);
            cs.newLineAtOffset(350, lineOffset - (i * 15));
            cs.showText(MenuItemQuantities.get(i).toString());
            cs.endText();

            cs.beginText();
            cs.setFont(PDType1Font.HELVETICA, 12);
            cs.newLineAtOffset(450, lineOffset - (i * 15));
            cs.showText(String.valueOf(MenuItemPrices.get(i) * MenuItemQuantities.get(i)));
            cs.endText();
        }

        // Total Amount
        cs.beginText();
        cs.setFont(PDType1Font.HELVETICA_BOLD, 12);
        cs.newLineAtOffset(350, lineOffset - (MenuItemNames.size() * 15) - 20);
        cs.showText("Grand Total: " + total);
        cs.endText();

        // Close the content stream and save the file
        cs.close();
        invc.save(FilePath);
        invc.close();

        // Print a success message to the console
        System.out.println("Invoice saved successfully to " + FilePath);
    }
}
