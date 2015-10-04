using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Factory
{
    //abstract class of units with/without queue
    public abstract class C_Unit
    {
        protected int ID_of_unit;

        protected double kpd { get; set; }
        protected double time_of_work { get; set; }

        public bool status { get; set; }                 // true if unit have the bid, false if it need to get the bid 
        public bool bid_is_ready { get; set; }           // true if bid is ready
        protected int processing_time { get; set; }      // time,which need to process the bid
        protected int time_count_for_bid { get; set; }
        protected int count_of_bid { get; set; }
        protected Random rnd = new Random();

        public int rand()
        {
            return rnd.Next(-1, 1);
        }
        public void recive_bid()
        { }
        public void push_bid()
        { }
        public void iterate()
        { }

    }
    public abstract class C_Unit_Queue : C_Unit
    {
        protected int max_in_queue;
        protected int[] queue_of_bids;
        protected int[] queue_time_of_bids;
        public bool queue_is_full;
    }

    //classes of units, which used in wire production
    public class C_Unit_1 : C_Unit //storage
    {
        public double KPD()
        {
            return Math.Round( kpd / time_of_work,5);
        }
        public void push_bid()
        {
            count_of_bid++;
            time_count_for_bid = 0;
            bid_is_ready = false;
            processing_time = 5 + rand();
        }
        public void iterate()
        {
            time_of_work++;
            if (time_count_for_bid == processing_time) bid_is_ready = true;
            if (status && bid_is_ready == false)
            {
                time_count_for_bid++;
                kpd++;
            }
        }
        public C_Unit_1(int ID, int seed)
        {
            this.ID_of_unit = ID;
            this.status = true;
            this.bid_is_ready = true;
            this.time_count_for_bid = 0;
            this.processing_time = 2 + rand();
            this.rnd = new Random(seed);
        }
    }
    public class C_Unit_2 : C_Unit //furnace
    {
        public double KPD()
        {
            return Math.Round(kpd / time_of_work, 5);
        }
        public void push_bid()
        {
            count_of_bid++;
            status = false;
            time_count_for_bid = 0;
            bid_is_ready = false;
        }
        public void iterate()
        {
            time_of_work++;
            if (time_count_for_bid == processing_time) bid_is_ready = true;
            if (status && bid_is_ready == false)
            {
                time_count_for_bid++;
                kpd++;
            }
        }
        public void recive_bid()
        {
            processing_time = 25 + rand();
            time_count_for_bid = 0;
            status = true;
        }
        public C_Unit_2(int ID, int seed)
        {
            this.ID_of_unit = ID;
            this.status = false;
            this.bid_is_ready = false;
            this.time_count_for_bid = 0;
            this.processing_time = 3;
            this.rnd = new Random(seed);
        }
    }
    public class C_Unit_3 : C_Unit // four-high stand
    {
        public double KPD()
        {
            return Math.Round(kpd / time_of_work, 5);
        }
        public void push_bid()
        {
            count_of_bid++;
            status = false;
            time_count_for_bid = 0;
            bid_is_ready = false;
        }
        public void iterate()
        {
            time_of_work++;
            if (time_count_for_bid == processing_time) bid_is_ready = true;
            if (status && bid_is_ready == false)
            {
                time_count_for_bid++;
                kpd++;
            }
        }
        public void recive_bid()
        {
            time_count_for_bid = 0;
            status = true;
        }
        public C_Unit_3(int ID, int seed)
        {
            this.ID_of_unit = ID;
            this.status = false;
            this.bid_is_ready = false;
            this.processing_time = 5;
            this.time_count_for_bid = 0;
            this.rnd = new Random(seed);
        }
    }
    public class C_Unit_4 : C_Unit // finishing scale breaker
    {
        public double KPD()
        {
            return Math.Round(kpd / time_of_work, 5);
        }
        public void push_bid()
        {
            count_of_bid++;
            status = false;
            time_count_for_bid = 0;
            bid_is_ready = false;
        }
        public void iterate()
        {
            time_of_work++;
            if (time_count_for_bid == processing_time) bid_is_ready = true;
            if (status && bid_is_ready == false)
            {
                time_count_for_bid++;
                kpd++;
            }
        }
        public void recive_bid()
        {
            time_count_for_bid = 0;
            status = true;
        }
        public C_Unit_4(int ID, int seed)
        {
            this.ID_of_unit = ID;
            this.status = false;
            this.bid_is_ready = false;
            this.processing_time = 5;
            this.time_count_for_bid = 0;
            this.rnd = new Random(seed);
        }

    }
    public class C_Unit_5 : C_Unit // descale sprays
    {
        public double KPD()
        {
            return Math.Round(kpd / time_of_work, 5);
        }
        public void push_bid()
        {
            count_of_bid++;
            status = false;
            time_count_for_bid = 0;
            bid_is_ready = false;
        }
        public void iterate()
        {
            time_of_work++;
            if (time_count_for_bid == processing_time) bid_is_ready = true;
            if (status && bid_is_ready == false)
            {
                time_count_for_bid++;
                kpd++;
            }
        }
        public void recive_bid()
        {
            time_count_for_bid = 0;
            status = true;
        }
        public C_Unit_5(int ID, int seed)
        {
            this.ID_of_unit = ID;
            this.status = false;
            this.bid_is_ready = false;
            this.processing_time = 5;
            this.time_count_for_bid = 0;
            this.rnd = new Random(seed);
        }

    }
    public class C_Unit_6 : C_Unit_Queue // fridge
    {
        public double KPD()
        {
            return Math.Round(kpd / time_of_work, 5);
        }
        /// <summary>
        /// Return the count of bids in queue
        /// </summary>
        int count_bid_in_queue()
        {
            int count_of_bids_in_queue = 0;
            for (int i = 0; i < max_in_queue; i++)
            {
                if (queue_of_bids[i] != -1) count_of_bids_in_queue++;
            }
            return count_of_bids_in_queue;
        }

        public void push_bid()
        {
            count_of_bid++;

            int pointer = 0; int count_of_ready_bids = 0;
            for (int i = 0; i < max_in_queue; i++)
            {
                if (queue_of_bids[i] == queue_time_of_bids[i]) { pointer = i; count_of_ready_bids++; }
            }
            if (count_of_ready_bids < 2) bid_is_ready = false;
            if (count_bid_in_queue() == 1) status = false;

            queue_of_bids[pointer] = -1;
            queue_is_full = false;
        }
        public void iterate()
        {
            time_of_work++;
            int qq = 0;
            for (int i = 0; i < max_in_queue; i++)
            {
                if (status && queue_of_bids[i] == queue_time_of_bids[i]) bid_is_ready = true;
                if (queue_of_bids[i] != -1 && queue_of_bids[i] < queue_time_of_bids[i])
                {
                    queue_of_bids[i]++;
                    qq++;
                }
            }
            if (qq > 0) kpd++;
        }
        public void recive_bid()
        {
            status = true;

            int pointer = 0; int count_of_bids_in_query = 0;

            for (int i = 0; i < max_in_queue; i++)
            {
                if (queue_of_bids[i] == -1) pointer = i;
                else { count_of_bids_in_query++; }
            }

            queue_time_of_bids[pointer] = processing_time + rand();
            queue_of_bids[pointer] = 0;

            if (count_of_bids_in_query == max_in_queue - 1) queue_is_full = true;
        }
        public C_Unit_6(int ID, int seed)
        {
            this.ID_of_unit = ID;
            this.status = false;
            this.bid_is_ready = false;
            this.time_count_for_bid = 0;
            this.rnd = new Random(seed);
            this.processing_time = 25;
            this.max_in_queue = 2;
            this.queue_of_bids = new int[max_in_queue];
            this.queue_time_of_bids = new int[max_in_queue];
            this.queue_is_full = false;
            for (int i = 0; i < max_in_queue; i++)
            {
                queue_of_bids[i] = -1;
            }

        }
    }
    public class C_Unit_7 : C_Unit // coiling machine
    {
        public double KPD()
        {
            return Math.Round(kpd / time_of_work, 5);
        }
        public void push_bid()
        {
            count_of_bid++;
            status = false;
            time_count_for_bid = 0;
            bid_is_ready = false;
        }
        public void iterate()
        {
            time_of_work++;
            if (time_count_for_bid == processing_time) bid_is_ready = true;
            if (status && bid_is_ready == false)
            {
                time_count_for_bid++;
                kpd++;
            }
        }
        public void recive_bid()
        {
            time_count_for_bid = 0;
            status = true;
        }
        public C_Unit_7(int ID, int seed)
        {
            this.ID_of_unit = ID;
            this.status = false;
            this.bid_is_ready = false;
            this.processing_time = 3;
            this.time_count_for_bid = 0;
            this.rnd = new Random(seed);
        }

    }
    public class C_Unit_8 : C_Unit_Queue // conveyor
    {
        public double KPD()
        {
            return Math.Round(kpd / time_of_work, 5);
        }
        /// <summary>
        /// Return the count of bids in queue
        /// </summary>
        int count_bid_in_queue()
        {
            int count_of_bids_in_queue = 0;
            for (int i = 0; i < max_in_queue; i++)
            {
                if (queue_of_bids[i] != -1) count_of_bids_in_queue++;
            }
            return count_of_bids_in_queue;
        }

        public void push_bid()
        {
            count_of_bid++;

            int pointer = 0; int count_of_ready_bids = 0;
            for (int i = 0; i < max_in_queue; i++)
            {
                if (queue_of_bids[i] == queue_time_of_bids[i]) { pointer = i; count_of_ready_bids++; }
            }

            if (count_of_ready_bids < 2) bid_is_ready = false;
            if (count_bid_in_queue() == 1) status = false;

            queue_of_bids[pointer] = -1;
            queue_is_full = false;
        }
        public void iterate()
        {
            time_of_work++;
            int qq = 0;
            for (int i = 0; i < max_in_queue; i++)
            {
                if (status && queue_of_bids[i] == queue_time_of_bids[i]) bid_is_ready = true;
                if (queue_of_bids[i] != -1 && queue_of_bids[i] < queue_time_of_bids[i])
                {
                    queue_of_bids[i]++;
                    qq++;
                }
            }
            if (qq > 0) kpd++;
        }
        public void recive_bid()
        {
            status = true;

            int pointer = 0; int count_of_bids_in_query = 0;

            for (int i = 0; i < max_in_queue; i++)
            {
                if (queue_of_bids[i] == -1) pointer = i;
                else { count_of_bids_in_query++; }
            }

            queue_time_of_bids[pointer] = processing_time + rand();
            queue_of_bids[pointer] = 0;

            if (count_of_bids_in_query == max_in_queue - 1) queue_is_full = true;
        }
        public C_Unit_8(int ID, int seed)
        {
            this.ID_of_unit = ID;
            this.status = false;
            this.bid_is_ready = false;
            this.processing_time = 6;
            this.time_count_for_bid = 0;
            this.rnd = new Random(seed);
            this.max_in_queue = 4;
            this.queue_of_bids = new int[max_in_queue];
            this.queue_time_of_bids = new int[max_in_queue];
            this.queue_is_full = false;

            for (int i = 0; i < max_in_queue; i++)
            {
                queue_of_bids[i] = -1;
            }
        }

    }
    public class C_Unit_9 : C_Unit // preservation machine
    {
        public double KPD()
        {
            return Math.Round(kpd / time_of_work, 5);
        }
        public void iterate()
        {
            time_of_work++;
            if (time_count_for_bid == processing_time) bid_is_ready = true;
            if (status && bid_is_ready == false)
            {
                time_count_for_bid++;
                kpd++;
            }
        }
        public void recive_bid()
        {
            processing_time = 4 + rand();
            time_count_for_bid = 0;
            status = true;
        }
        public void finish_bid()
        {
            count_of_bid++;
            status = false;
            time_count_for_bid = 0;
            bid_is_ready = false;
        }
        public int Count_of_bid()
        {
            return count_of_bid;
        }
        public C_Unit_9(int ID, int seed)
        {
            this.ID_of_unit = ID;
            this.status = false;
            this.bid_is_ready = false;
            this.time_count_for_bid = 0;
            this.rnd = new Random(seed);
            this.processing_time = 4;
        }

    }


    class C_Factory // class simulates the work of the plant and the relationship between units
    {
        //fields
        protected int time = 0; // minutes
        //StreamWriter sw = new StreamWriter("C:\\factory_log.txt");
        Random random_seeds = new Random();

        C_Unit_1 U1;

        C_Unit_2 U2_1;
        C_Unit_2 U2_2;
        C_Unit_2 U2_3;
        C_Unit_2 U2_4;
        C_Unit_2 U2_5;

        C_Unit_3 U3_1;
        C_Unit_3 U3_2;
        C_Unit_3 U3_3;
        C_Unit_3 U3_4;
        C_Unit_3 U3_5;
        C_Unit_3 U3_6;
        C_Unit_3 U3_7;

        C_Unit_4 U4;
        C_Unit_5 U5;

        C_Unit_6 U6_1;
        C_Unit_6 U6_2;
        C_Unit_6 U6_3;

        C_Unit_7 U7;
        C_Unit_8 U8;
        C_Unit_9 U9;

        Form1 form;


        void inicialize()
        {
           
            U1 = new C_Unit_1(1, random_seeds.Next(100000));

            U2_1 = new C_Unit_2(2, random_seeds.Next(100000));
            U2_2 = new C_Unit_2(3, random_seeds.Next(100000));
            U2_3 = new C_Unit_2(4, random_seeds.Next(100000));
            U2_4 = new C_Unit_2(5, random_seeds.Next(100000));
            U2_5 = new C_Unit_2(6, random_seeds.Next(100000));

            U3_1 = new C_Unit_3(7, random_seeds.Next(100000));
            U3_2 = new C_Unit_3(8, random_seeds.Next(100000));
            U3_3 = new C_Unit_3(9, random_seeds.Next(100000));
            U3_4 = new C_Unit_3(10, random_seeds.Next(100000));
            U3_5 = new C_Unit_3(11, random_seeds.Next(100000));
            U3_6 = new C_Unit_3(12, random_seeds.Next(100000));
            U3_7 = new C_Unit_3(13, random_seeds.Next(100000));

            U4 = new C_Unit_4(14, random_seeds.Next(100000));
            U5 = new C_Unit_5(15, random_seeds.Next(100000));

            U6_1 = new C_Unit_6(16, random_seeds.Next(100000));
            U6_2 = new C_Unit_6(17, random_seeds.Next(100000));
            U6_3 = new C_Unit_6(18, random_seeds.Next(100000));

            U7 = new C_Unit_7(19, random_seeds.Next(100000));
            U8 = new C_Unit_8(20, random_seeds.Next(100000));
            U9 = new C_Unit_9(21, random_seeds.Next(100000));
        }

        /* The bid is one of raw materials, sequentially
         passes through all nodes of the process. */

        void processing_bids()
        {
            U1.iterate();
            U2_1.iterate();
            U2_2.iterate();
            U2_3.iterate();
            U2_4.iterate();
            U2_5.iterate();

            U3_1.iterate();
            U3_2.iterate();
            U3_3.iterate();
            U3_4.iterate();
            U3_5.iterate();
            U3_6.iterate();
            U3_7.iterate();

            U4.iterate();
            U5.iterate();

            U6_1.iterate();
            U6_2.iterate();
            U6_3.iterate();

            U7.iterate();
            U8.iterate();
            U9.iterate();
        }
        void transfer_bids()
        {
            
            if (U9.bid_is_ready) U9.finish_bid();
            if (U8.bid_is_ready && U9.status == false) { U8.push_bid(); U9.recive_bid(); }

            if (U7.bid_is_ready && U8.queue_is_full == false) { U7.push_bid(); U8.recive_bid(); }

            if (U6_1.bid_is_ready && U7.status == false) { U6_1.push_bid(); U7.recive_bid(); }
            if (U6_2.bid_is_ready && U7.status == false) { U6_2.push_bid(); U7.recive_bid(); }
            if (U6_3.bid_is_ready && U7.status == false) { U6_3.push_bid(); U7.recive_bid(); }

            if (U5.bid_is_ready && U6_1.queue_is_full == false) { U5.push_bid(); U6_1.recive_bid(); }
            if (U5.bid_is_ready && U6_2.queue_is_full == false) { U5.push_bid(); U6_2.recive_bid(); }
            if (U5.bid_is_ready && U6_3.queue_is_full == false) { U5.push_bid(); U6_3.recive_bid(); }

            if (U4.bid_is_ready && U5.status == false) { U4.push_bid(); U5.recive_bid(); }

            if (U3_7.bid_is_ready && U4.status == false) { U3_7.push_bid(); U4.recive_bid(); }
            if (U3_6.bid_is_ready && U3_7.status == false) { U3_6.push_bid(); U3_7.recive_bid(); }
            if (U3_5.bid_is_ready && U3_6.status == false) { U3_5.push_bid(); U3_6.recive_bid(); }
            if (U3_4.bid_is_ready && U3_5.status == false) { U3_4.push_bid(); U3_5.recive_bid(); }
            if (U3_3.bid_is_ready && U3_4.status == false) { U3_3.push_bid(); U3_4.recive_bid(); }
            if (U3_2.bid_is_ready && U3_3.status == false) { U3_2.push_bid(); U3_3.recive_bid(); }
            if (U3_1.bid_is_ready && U3_2.status == false) { U3_1.push_bid(); U3_2.recive_bid(); }

            if (U2_1.bid_is_ready && U3_1.status == false) { U2_1.push_bid(); U3_1.recive_bid(); }
            if (U2_2.bid_is_ready && U3_1.status == false) { U2_2.push_bid(); U3_1.recive_bid(); }
            if (U2_3.bid_is_ready && U3_1.status == false) { U2_3.push_bid(); U3_1.recive_bid(); }
            if (U2_4.bid_is_ready && U3_1.status == false) { U2_4.push_bid(); U3_1.recive_bid(); }
            if (U2_5.bid_is_ready && U3_1.status == false) { U2_5.push_bid(); U3_1.recive_bid(); }

            if (U1.bid_is_ready && U2_1.status == false) { U1.push_bid(); U2_1.recive_bid(); }
            if (U1.bid_is_ready && U2_2.status == false) { U1.push_bid(); U2_2.recive_bid(); }
            if (U1.bid_is_ready && U2_3.status == false) { U1.push_bid(); U2_3.recive_bid(); }
            if (U1.bid_is_ready && U2_4.status == false) { U1.push_bid(); U2_4.recive_bid(); }
            if (U1.bid_is_ready && U2_5.status == false) { U1.push_bid(); U2_5.recive_bid(); }


        }
        void show_graphs()
        {            
            if (U1.status && U1.bid_is_ready == false) form.pictureBox1.BackColor = System.Drawing.Color.Lime;
            if (U1.bid_is_ready) form.pictureBox1.BackColor = System.Drawing.Color.Yellow;

            if (U2_1.status == false) form.pictureBox2.BackColor = System.Drawing.Color.Red;
            if (U2_1.status && U2_1.bid_is_ready == false) form.pictureBox2.BackColor = System.Drawing.Color.Lime;
            if (U2_1.bid_is_ready) form.pictureBox2.BackColor = System.Drawing.Color.Yellow;

            if (U2_2.status == false) form.pictureBox3.BackColor = System.Drawing.Color.Red;
            if (U2_2.status && U2_2.bid_is_ready == false) form.pictureBox3.BackColor = System.Drawing.Color.Lime;
            if (U2_2.bid_is_ready) form.pictureBox3.BackColor = System.Drawing.Color.Yellow;

            if (U2_3.status == false) form.pictureBox4.BackColor = System.Drawing.Color.Red;
            if (U2_3.status && U2_3.bid_is_ready == false) form.pictureBox4.BackColor = System.Drawing.Color.Lime;
            if (U2_3.bid_is_ready) form.pictureBox4.BackColor = System.Drawing.Color.Yellow;

            if (U2_4.status == false) form.pictureBox20.BackColor = System.Drawing.Color.Red;
            if (U2_4.status && U2_4.bid_is_ready == false) form.pictureBox20.BackColor = System.Drawing.Color.Lime;
            if (U2_4.bid_is_ready) form.pictureBox20.BackColor = System.Drawing.Color.Yellow;

            if (U2_5.status == false) form.pictureBox21.BackColor = System.Drawing.Color.Red;
            if (U2_5.status && U2_5.bid_is_ready == false) form.pictureBox21.BackColor = System.Drawing.Color.Lime;
            if (U2_5.bid_is_ready) form.pictureBox21.BackColor = System.Drawing.Color.Yellow;

            if (U3_1.status == false) form.pictureBox5.BackColor = System.Drawing.Color.Red;
            if (U3_1.status && U3_1.bid_is_ready == false) form.pictureBox5.BackColor = System.Drawing.Color.Lime;
            if (U3_1.bid_is_ready) form.pictureBox5.BackColor = System.Drawing.Color.Yellow;

            if (U3_2.status == false) form.pictureBox6.BackColor = System.Drawing.Color.Red;
            if (U3_2.status && U3_2.bid_is_ready == false) form.pictureBox6.BackColor = System.Drawing.Color.Lime;
            if (U3_2.bid_is_ready) form.pictureBox6.BackColor = System.Drawing.Color.Yellow;

            if (U3_3.status == false) form.pictureBox7.BackColor = System.Drawing.Color.Red;
            if (U3_3.status && U3_3.bid_is_ready == false) form.pictureBox7.BackColor = System.Drawing.Color.Lime;
            if (U3_3.bid_is_ready) form.pictureBox7.BackColor = System.Drawing.Color.Yellow;

            if (U3_4.status == false) form.pictureBox8.BackColor = System.Drawing.Color.Red;
            if (U3_4.status && U3_4.bid_is_ready == false) form.pictureBox8.BackColor = System.Drawing.Color.Lime;
            if (U3_4.bid_is_ready) form.pictureBox8.BackColor = System.Drawing.Color.Yellow;

            if (U3_5.status == false) form.pictureBox9.BackColor = System.Drawing.Color.Red;
            if (U3_5.status && U3_5.bid_is_ready == false) form.pictureBox9.BackColor = System.Drawing.Color.Lime;
            if (U3_5.bid_is_ready) form.pictureBox9.BackColor = System.Drawing.Color.Yellow;

            if (U3_6.status == false) form.pictureBox10.BackColor = System.Drawing.Color.Red;
            if (U3_6.status && U3_6.bid_is_ready == false) form.pictureBox10.BackColor = System.Drawing.Color.Lime;
            if (U3_6.bid_is_ready) form.pictureBox10.BackColor = System.Drawing.Color.Yellow;

            if (U3_7.status == false) form.pictureBox11.BackColor = System.Drawing.Color.Red;
            if (U3_7.status && U3_7.bid_is_ready == false) form.pictureBox11.BackColor = System.Drawing.Color.Lime;
            if (U3_7.bid_is_ready) form.pictureBox11.BackColor = System.Drawing.Color.Yellow;

            if (U4.status == false) form.pictureBox12.BackColor = System.Drawing.Color.Red;
            if (U4.status && U4.bid_is_ready == false) form.pictureBox12.BackColor = System.Drawing.Color.Lime;
            if (U4.bid_is_ready) form.pictureBox12.BackColor = System.Drawing.Color.Yellow;

            if (U5.status == false) form.pictureBox13.BackColor = System.Drawing.Color.Red;
            if (U5.status && U5.bid_is_ready == false) form.pictureBox13.BackColor = System.Drawing.Color.Lime;
            if (U5.bid_is_ready) form.pictureBox13.BackColor = System.Drawing.Color.Yellow;

            if (U6_1.status == false) form.pictureBox14.BackColor = System.Drawing.Color.Red;
            if (U6_1.status && U6_1.bid_is_ready == false) form.pictureBox14.BackColor = System.Drawing.Color.Lime;
            if (U6_1.bid_is_ready) form.pictureBox14.BackColor = System.Drawing.Color.Yellow;

            if (U6_2.status == false) form.pictureBox15.BackColor = System.Drawing.Color.Red;
            if (U6_2.status && U6_2.bid_is_ready == false) form.pictureBox15.BackColor = System.Drawing.Color.Lime;
            if (U6_2.bid_is_ready) form.pictureBox15.BackColor = System.Drawing.Color.Yellow;

            if (U6_3.status == false) form.pictureBox16.BackColor = System.Drawing.Color.Red;
            if (U6_3.status && U6_3.bid_is_ready == false) form.pictureBox16.BackColor = System.Drawing.Color.Lime;
            if (U6_3.bid_is_ready) form.pictureBox16.BackColor = System.Drawing.Color.Yellow;

            if (U7.status == false) form.pictureBox17.BackColor = System.Drawing.Color.Red;
            if (U7.status && U7.bid_is_ready == false) form.pictureBox17.BackColor = System.Drawing.Color.Lime;
            if (U7.bid_is_ready) form.pictureBox17.BackColor = System.Drawing.Color.Yellow;

            if (U8.status == false) form.pictureBox18.BackColor = System.Drawing.Color.Red;
            if (U8.status && U8.bid_is_ready == false) form.pictureBox18.BackColor = System.Drawing.Color.Lime;
            if (U8.bid_is_ready) form.pictureBox18.BackColor = System.Drawing.Color.Yellow;

            if (U9.status == false) form.pictureBox19.BackColor = System.Drawing.Color.Red;
            if (U9.status && U9.bid_is_ready == false) form.pictureBox19.BackColor = System.Drawing.Color.Lime;
            if (U9.bid_is_ready) form.pictureBox19.BackColor = System.Drawing.Color.Yellow;
        }

        public void statist()
        {
            double a = U9.Count_of_bid();
            double b = time;
            form.Write_Text25(Math.Round(b / a, 5).ToString());

            form.Write_Text4(U1.KPD().ToString());
            form.Write_Text5(U2_1.KPD().ToString());
            form.Write_Text6(U2_2.KPD().ToString());
            form.Write_Text7(U2_3.KPD().ToString());
            form.Write_Text8(U2_4.KPD().ToString());
            form.Write_Text9(U2_5.KPD().ToString());
            form.Write_Text10(U3_1.KPD().ToString());
            form.Write_Text11(U3_2.KPD().ToString());
            form.Write_Text12(U3_3.KPD().ToString());
            form.Write_Text13(U3_4.KPD().ToString());
            form.Write_Text14(U3_5.KPD().ToString());
            form.Write_Text15(U3_6.KPD().ToString());
            form.Write_Text16(U3_7.KPD().ToString());
            form.Write_Text17(U4.KPD().ToString());
            form.Write_Text18(U5.KPD().ToString());
            form.Write_Text19(U6_1.KPD().ToString());
            form.Write_Text20(U6_2.KPD().ToString());
            form.Write_Text21(U6_3.KPD().ToString());
            form.Write_Text22(U7.KPD().ToString());
            form.Write_Text23(U8.KPD().ToString());
            form.Write_Text24(U9.KPD().ToString());
        }
        public void iterate()
        {
            processing_bids();
            transfer_bids();
            show_graphs();
            time++;
            form.Write_Text1(U9.Count_of_bid().ToString());
            form.Write_Text2(time.ToString());
        }

        public C_Factory(Form1 form)
        {
            inicialize();
            this.form = form;
        }
    }

    static class Program
    {
        [STAThread]

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

        }
    }
}
