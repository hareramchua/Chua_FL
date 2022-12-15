using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotFuzzy;
namespace AliacFuzzyLogic
{
    public partial class Form1 : Form
    {
        FuzzyEngine fe;
        MembershipFunctionCollection speed, meter, throttle;
        LinguisticVariable myspeed, mymeter, mythrottle;
        FuzzyRuleCollection myrules;


        public Form1()
        {
            InitializeComponent();
        }


        public void setMembers()
        {

            speed = new MembershipFunctionCollection();
            speed.Add(new MembershipFunction("LOW", 0.0, 0.0, 45.0, 50.0));
            speed.Add(new MembershipFunction("OK", 45.0, 50.0, 50.0, 55.0));
            speed.Add(new MembershipFunction("HIGH", 50.0, 55.0, 100.0, 100.0));
            myspeed = new LinguisticVariable("SPEED", speed);


            meter = new MembershipFunctionCollection();
            meter.Add(new MembershipFunction("NEAR", 0.0, 15.0, 15.0, 24.0));
            meter.Add(new MembershipFunction("AVG", 25.0, 30.0, 30.0, 49.0));
            meter.Add(new MembershipFunction("FAR", 50.0, 60.0, 60.0, 100.0));
            mymeter = new LinguisticVariable("METER", meter);

            throttle = new MembershipFunctionCollection();
            throttle.Add(new MembershipFunction("LOW", 0.0, 0.0, 2.0, 4.0));
            throttle.Add(new MembershipFunction("LM", 2.0, 4.0, 4.0, 6.0));
            throttle.Add(new MembershipFunction("MED", 4.0, 6.0, 6.0, 8.0));
            throttle.Add(new MembershipFunction("HM", 6.0, 8.0, 8.0, 10.0));
            throttle.Add(new MembershipFunction("HIGH", 8.0, 10.0, 10.0, 10.0));
            mythrottle = new LinguisticVariable("THROTTLE", throttle);



        }

        public void setRules()
        {
            myrules = new FuzzyRuleCollection();
            myrules.Add(new FuzzyRule("IF (SPEED IS HIGH) AND (METER IS FAR) THEN THROTTLE IS LM"));
            myrules.Add(new FuzzyRule("IF (SPEED IS HIGH) AND (METER IS AVG) THEN THROTTLE IS LM"));
            myrules.Add(new FuzzyRule("IF (SPEED IS HIGH) AND (METER IS NEAR) THEN THROTTLE IS LOW"));
            myrules.Add(new FuzzyRule("IF (SPEED IS OK) AND (METER IS FAR) THEN THROTTLE IS HM"));
            myrules.Add(new FuzzyRule("IF (SPEED IS OK) AND (METER IS AVG) THEN THROTTLE IS MED"));
            myrules.Add(new FuzzyRule("IF (SPEED IS OK) AND (METER IS NEAR) THEN THROTTLE IS LM"));
            myrules.Add(new FuzzyRule("IF (SPEED IS LOW) AND (METER IS FAR) THEN THROTTLE IS HIGH"));
            myrules.Add(new FuzzyRule("IF (SPEED IS OK) AND (METER IS AVG) THEN THROTTLE IS HM"));
            myrules.Add(new FuzzyRule("IF (SPEED IS OK) AND (METER IS NEAR) THEN THROTTLE IS HM"));
        }

        public void setFuzzyEngine()
        {
            fe = new FuzzyEngine();
            fe.LinguisticVariableCollection.Add(myspeed);
            fe.LinguisticVariableCollection.Add(mymeter);
            fe.LinguisticVariableCollection.Add(mythrottle);
            fe.FuzzyRuleCollection = myrules;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void defuziffyToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setMembers();
            setRules();
            //setFuzzyEngine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myspeed.InputValue = (Convert.ToDouble(textBox1.Text));
            myspeed.Fuzzify("OK");



        }
        private void button2_Click(object sender, EventArgs e)
        {
            mymeter.InputValue = (Convert.ToDouble(textBox2.Text));
            mymeter.Fuzzify("AVG");

        }

        public void fuziffyvalues()
        {
            myspeed.InputValue = (Convert.ToDouble(textBox1.Text));
            myspeed.Fuzzify("LOW");
            mymeter.InputValue = (Convert.ToDouble(textBox2.Text));
            mymeter.Fuzzify("NEAR");

        }
        public void defuzzy()
        {
            setFuzzyEngine();
            fe.Consequent = "THROTTLE";
            textBox3.Text = "" + fe.Defuzzify();
        }

        public void computenewspeed()
        {

            double oldspeed = Convert.ToDouble(textBox1.Text);
            double oldthrottle = Convert.ToDouble(textBox3.Text);
            double oldangle = Convert.ToDouble(textBox2.Text);
            double newspeed = ((1 - 0.1) * (oldspeed)) + (oldthrottle - (0.1 * oldangle));
            textBox1.Text = "" + newspeed;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            setFuzzyEngine();
            fe.Consequent = "THROTTLE";
            textBox3.Text = "" + fe.Defuzzify();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            computenewspeed();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            setMembers();
            setRules();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            fuziffyvalues();
            defuzzy();
            computenewspeed();
        }


    }
}
