namespace BoardMate.Models
{
    partial class SideNavigation
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SideNavigation));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnBoarder = new System.Windows.Forms.Button();
            this.btnContract = new System.Windows.Forms.Button();
            this.btnInvoice = new System.Windows.Forms.Button();
            this.btnPayment = new System.Windows.Forms.Button();
            this.btnSMS = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelBtnRoom = new System.Windows.Forms.Panel();
            this.btnRooms = new System.Windows.Forms.Button();
            this.btnManageRooms = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnRoomType = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panelBtnRoom.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel1.Controls.Add(this.btnDashboard);
            this.flowLayoutPanel1.Controls.Add(this.btnBoarder);
            this.flowLayoutPanel1.Controls.Add(this.panelBtnRoom);
            this.flowLayoutPanel1.Controls.Add(this.btnContract);
            this.flowLayoutPanel1.Controls.Add(this.btnInvoice);
            this.flowLayoutPanel1.Controls.Add(this.btnPayment);
            this.flowLayoutPanel1.Controls.Add(this.btnSMS);
            this.flowLayoutPanel1.Controls.Add(this.btnSettings);
            this.flowLayoutPanel1.Controls.Add(this.btnLogout);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(200, 802);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(136)))), ((int)(((byte)(216)))));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 95);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btnDashboard
            // 
            this.btnDashboard.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDashboard.Image = ((System.Drawing.Image)(resources.GetObject("btnDashboard.Image")));
            this.btnDashboard.Location = new System.Drawing.Point(0, 101);
            this.btnDashboard.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(200, 50);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "   Dashboard";
            this.btnDashboard.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnDashboard.UseVisualStyleBackColor = false;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // btnBoarder
            // 
            this.btnBoarder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBoarder.FlatAppearance.BorderSize = 0;
            this.btnBoarder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBoarder.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBoarder.Image = ((System.Drawing.Image)(resources.GetObject("btnBoarder.Image")));
            this.btnBoarder.Location = new System.Drawing.Point(0, 157);
            this.btnBoarder.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnBoarder.Name = "btnBoarder";
            this.btnBoarder.Size = new System.Drawing.Size(200, 50);
            this.btnBoarder.TabIndex = 3;
            this.btnBoarder.Text = "   Boarder    ";
            this.btnBoarder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBoarder.UseVisualStyleBackColor = true;
            this.btnBoarder.Click += new System.EventHandler(this.btnBoarder_Click);
            // 
            // btnContract
            // 
            this.btnContract.FlatAppearance.BorderSize = 0;
            this.btnContract.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnContract.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnContract.Image = ((System.Drawing.Image)(resources.GetObject("btnContract.Image")));
            this.btnContract.Location = new System.Drawing.Point(0, 263);
            this.btnContract.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnContract.Name = "btnContract";
            this.btnContract.Size = new System.Drawing.Size(200, 50);
            this.btnContract.TabIndex = 4;
            this.btnContract.Text = "   Contract   ";
            this.btnContract.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnContract.UseVisualStyleBackColor = true;
            this.btnContract.Click += new System.EventHandler(this.btnContract_Click);
            // 
            // btnInvoice
            // 
            this.btnInvoice.FlatAppearance.BorderSize = 0;
            this.btnInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInvoice.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInvoice.Image = ((System.Drawing.Image)(resources.GetObject("btnInvoice.Image")));
            this.btnInvoice.Location = new System.Drawing.Point(0, 319);
            this.btnInvoice.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnInvoice.Name = "btnInvoice";
            this.btnInvoice.Size = new System.Drawing.Size(200, 50);
            this.btnInvoice.TabIndex = 5;
            this.btnInvoice.Text = "    Invoice        ";
            this.btnInvoice.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnInvoice.UseVisualStyleBackColor = true;
            this.btnInvoice.Click += new System.EventHandler(this.btnInvoice_Click);
            // 
            // btnPayment
            // 
            this.btnPayment.FlatAppearance.BorderSize = 0;
            this.btnPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayment.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayment.Image = ((System.Drawing.Image)(resources.GetObject("btnPayment.Image")));
            this.btnPayment.Location = new System.Drawing.Point(0, 375);
            this.btnPayment.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnPayment.Name = "btnPayment";
            this.btnPayment.Size = new System.Drawing.Size(200, 50);
            this.btnPayment.TabIndex = 6;
            this.btnPayment.Text = "    Payment    ";
            this.btnPayment.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPayment.UseVisualStyleBackColor = true;
            this.btnPayment.Click += new System.EventHandler(this.btnPayment_Click);
            // 
            // btnSMS
            // 
            this.btnSMS.FlatAppearance.BorderSize = 0;
            this.btnSMS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSMS.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSMS.Image = ((System.Drawing.Image)(resources.GetObject("btnSMS.Image")));
            this.btnSMS.Location = new System.Drawing.Point(0, 431);
            this.btnSMS.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnSMS.Name = "btnSMS";
            this.btnSMS.Size = new System.Drawing.Size(200, 50);
            this.btnSMS.TabIndex = 8;
            this.btnSMS.Text = "    SMS         ";
            this.btnSMS.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSMS.UseVisualStyleBackColor = true;
            this.btnSMS.Click += new System.EventHandler(this.btnSMS_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSettings.Image = ((System.Drawing.Image)(resources.GetObject("btnSettings.Image")));
            this.btnSettings.Location = new System.Drawing.Point(0, 487);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(200, 50);
            this.btnSettings.TabIndex = 7;
            this.btnSettings.Text = "    Settings    ";
            this.btnSettings.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Image = ((System.Drawing.Image)(resources.GetObject("btnLogout.Image")));
            this.btnLogout.Location = new System.Drawing.Point(0, 543);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(200, 50);
            this.btnLogout.TabIndex = 9;
            this.btnLogout.Text = "    Logout    ";
            this.btnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panelBtnRoom
            // 
            this.panelBtnRoom.Controls.Add(this.btnRoomType);
            this.panelBtnRoom.Controls.Add(this.btnManageRooms);
            this.panelBtnRoom.Controls.Add(this.btnRooms);
            this.panelBtnRoom.Location = new System.Drawing.Point(0, 210);
            this.panelBtnRoom.Margin = new System.Windows.Forms.Padding(0);
            this.panelBtnRoom.MaximumSize = new System.Drawing.Size(200, 127);
            this.panelBtnRoom.MinimumSize = new System.Drawing.Size(200, 50);
            this.panelBtnRoom.Name = "panelBtnRoom";
            this.panelBtnRoom.Size = new System.Drawing.Size(200, 50);
            this.panelBtnRoom.TabIndex = 10;
            // 
            // btnRooms
            // 
            this.btnRooms.FlatAppearance.BorderSize = 0;
            this.btnRooms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRooms.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRooms.Image = ((System.Drawing.Image)(resources.GetObject("btnRooms.Image")));
            this.btnRooms.Location = new System.Drawing.Point(0, 0);
            this.btnRooms.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnRooms.Name = "btnRooms";
            this.btnRooms.Size = new System.Drawing.Size(200, 50);
            this.btnRooms.TabIndex = 3;
            this.btnRooms.Text = "  Rooms        ⮟";
            this.btnRooms.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRooms.UseVisualStyleBackColor = true;
            this.btnRooms.Click += new System.EventHandler(this.btnRooms_Click);
            // 
            // btnManageRooms
            // 
            this.btnManageRooms.FlatAppearance.BorderSize = 0;
            this.btnManageRooms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageRooms.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageRooms.Location = new System.Drawing.Point(0, 56);
            this.btnManageRooms.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnManageRooms.Name = "btnManageRooms";
            this.btnManageRooms.Size = new System.Drawing.Size(200, 32);
            this.btnManageRooms.TabIndex = 4;
            this.btnManageRooms.Text = "Manage Rooms";
            this.btnManageRooms.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnManageRooms.UseVisualStyleBackColor = true;
            this.btnManageRooms.Click += new System.EventHandler(this.btnManageRooms_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 15;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnRoomType
            // 
            this.btnRoomType.FlatAppearance.BorderSize = 0;
            this.btnRoomType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRoomType.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRoomType.Location = new System.Drawing.Point(0, 94);
            this.btnRoomType.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.btnRoomType.Name = "btnRoomType";
            this.btnRoomType.Size = new System.Drawing.Size(200, 32);
            this.btnRoomType.TabIndex = 5;
            this.btnRoomType.Text = "Room Type";
            this.btnRoomType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRoomType.UseVisualStyleBackColor = true;
            this.btnRoomType.Click += new System.EventHandler(this.btnRoomType_Click);
            // 
            // SideNavigation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "SideNavigation";
            this.Size = new System.Drawing.Size(200, 802);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panelBtnRoom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnBoarder;
        private System.Windows.Forms.Button btnContract;
        private System.Windows.Forms.Button btnInvoice;
        private System.Windows.Forms.Button btnPayment;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnSMS;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelBtnRoom;
        private System.Windows.Forms.Button btnManageRooms;
        private System.Windows.Forms.Button btnRooms;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnRoomType;
    }
}
