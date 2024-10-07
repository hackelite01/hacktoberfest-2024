import javax.swing.*;

import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class FoodC implements ActionListener {
    JFrame f1;
    JTextField tfPaneerQty, tfPulavQty, tfChickenQty, tfMuttonQty, tfFishQty;
    JCheckBox cbPaneer, cbPulav, cbChicken, cbMutton, cbFish;
    JLabel lTitle, lMenu, lCostLabel, lQtyLabel;
    JLabel lPaneerPrice, lPulavPrice, lChickenPrice, lMuttonPrice, lFishPrice;
    JButton bOrder, bCancel;
    JProgressBar br;

    public FoodC() {
        f1 = new JFrame("Food Corner");
        f1.setSize(600, 500);
        f1.setLayout(null);
        
        br=new JProgressBar();
		br.setOrientation(0);
		br.setBounds(100, 20, 400, 60);
		br.setBackground(Color.yellow);
		br.setFont(new Font("Arial",Font.BOLD,50));
		br.setForeground(Color.orange);
		br.setIndeterminate(true);
		br.setString("FOOD CORNER");
		br.setStringPainted (true);
		f1.add(br);
		
		lMenu = new JLabel("MENU");
        lMenu.setBounds(60, 70, 100, 50);
        lMenu.setFont(new Font("Arial", Font.BOLD, 15));
        f1.add(lMenu);

        cbPaneer = new JCheckBox("Paneer");
        cbPaneer.setBounds(50, 110, 100, 50);
        f1.add(cbPaneer);

        cbPulav = new JCheckBox("Pulav");
        cbPulav.setBounds(50, 150, 100, 50);
        f1.add(cbPulav);

        cbChicken = new JCheckBox("Chicken");
        cbChicken.setBounds(50, 190, 100, 50);
        f1.add(cbChicken);

        cbMutton = new JCheckBox("Mutton");
        cbMutton.setBounds(50, 230, 100, 50);
        f1.add(cbMutton);

        cbFish = new JCheckBox("Fish");
        cbFish.setBounds(50, 270, 100, 50);
        f1.add(cbFish);
        
        lCostLabel = new JLabel("COST");
        lCostLabel.setBounds(240, 70, 100, 50);
        lCostLabel.setFont(new Font("Arial", Font.BOLD, 15));
        f1.add(lCostLabel);

        lPaneerPrice = new JLabel("180/-");
        lPaneerPrice.setBounds(250, 110, 100, 50);
        f1.add(lPaneerPrice);

        lPulavPrice = new JLabel("120/-");
        lPulavPrice.setBounds(250, 150, 100, 50);
        f1.add(lPulavPrice);

        lChickenPrice = new JLabel("150/-");
        lChickenPrice.setBounds(250, 190, 100, 50);
        f1.add(lChickenPrice);

        lMuttonPrice = new JLabel("380/-");
        lMuttonPrice.setBounds(250, 230, 100, 50);
        f1.add(lMuttonPrice);

        lFishPrice = new JLabel("180/-");
        lFishPrice.setBounds(250, 270, 100, 50);
        f1.add(lFishPrice);

        tfPaneerQty = new JTextField("0");
        tfPaneerQty.setBounds(400, 120, 60, 30);
        f1.add(tfPaneerQty);
        
        lQtyLabel = new JLabel("QTY");
        lQtyLabel.setBounds(410, 70, 100, 50);
        lQtyLabel.setFont(new Font("Arial", Font.BOLD, 15));
        f1.add(lQtyLabel);

        tfPulavQty = new JTextField("0");
        tfPulavQty.setBounds(400, 160, 60, 30);
        f1.add(tfPulavQty);

        tfChickenQty = new JTextField("0");
        tfChickenQty.setBounds(400, 200, 60, 30);
        f1.add(tfChickenQty);

        tfMuttonQty = new JTextField("0");
        tfMuttonQty.setBounds(400, 240, 60, 30);
        f1.add(tfMuttonQty);

        tfFishQty = new JTextField("0");
        tfFishQty.setBounds(400, 280, 60, 30);
        f1.add(tfFishQty);

        bOrder = new JButton("Order");
        bOrder.setBounds(200, 400, 100, 30);
        f1.add(bOrder);

        bCancel = new JButton("Cancel");
        bCancel.setBounds(320, 400, 100, 30);
        f1.add(bCancel);

        bOrder.addActionListener(this);
        bCancel.addActionListener(this);

        f1.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        f1.setVisible(true);
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        if (e.getSource() == bOrder) {
            try {
                generatePDFBill();
                JOptionPane.showMessageDialog(f1, "Invoice Generated Successfully!", "Success", JOptionPane.INFORMATION_MESSAGE);
            } catch (Exception ex) {
                ex.printStackTrace();
                JOptionPane.showMessageDialog(f1, "Failed to generate the invoice.", "Error", JOptionPane.ERROR_MESSAGE);
            }
        } else if (e.getSource() == bCancel) {
            resetFieldsAndCheckBoxes();
        }
    }

    private void generatePDFBill() throws Exception {
    	wDB billing = new wDB();
        billing.setCustomerInfo("Customer Name", "1234567890"); 

        if (cbPaneer.isSelected()) {
            int qty = Integer.parseInt(tfPaneerQty.getText());
            billing.addMenuItem("Paneer", 180, qty);
        }
        if (cbPulav.isSelected()) {
            int qty = Integer.parseInt(tfPulavQty.getText());
            billing.addMenuItem("Pulav", 120, qty);
        }
        if (cbChicken.isSelected()) {
            int qty = Integer.parseInt(tfChickenQty.getText());
            billing.addMenuItem("Chicken", 150, qty);
        }
        if (cbMutton.isSelected()) {
            int qty = Integer.parseInt(tfMuttonQty.getText());
            billing.addMenuItem("Mutton", 200, qty);
        }
        if (cbFish.isSelected()) {
            int qty = Integer.parseInt(tfFishQty.getText());
            billing.addMenuItem("Fish", 180, qty);
        }

        billing.writeInvoice();
    }

    private void resetFieldsAndCheckBoxes() {
        JCheckBox[] checkBoxes = {cbPaneer, cbPulav, cbChicken, cbMutton, cbFish};
        JTextField[] fields = {tfPaneerQty, tfPulavQty, tfChickenQty, tfMuttonQty, tfFishQty};

        for (JTextField field : fields) {
            field.setText("0");
        }
        for (JCheckBox checkBox : checkBoxes) {
            checkBox.setSelected(false);
        }
    }

    public static void main(String[] args) {
    	SwingUtilities.invokeLater(new Runnable() {
            public void run() {
                new FoodC();
            }
    });
    
}}
