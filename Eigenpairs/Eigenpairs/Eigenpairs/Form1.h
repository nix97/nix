#pragma once

namespace CppCLRWinFormsProject {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
#include <string>


	/// <summary>
	/// Summary for Form1
	/// </summary>
	public ref class Form1 : public System::Windows::Forms::Form
	{
	public:
		Form1(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~Form1()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::TextBox^ tb_n;
	private: System::Windows::Forms::Button^ butExample;
	private: System::Windows::Forms::DataGridView^ dgv;
	private: System::Windows::Forms::Label^ label3;
	private: System::Windows::Forms::TextBox^ tb_epsilon;

	private: System::Windows::Forms::Label^ label4;
	private: System::Windows::Forms::Button^ butRun;
	private: System::Windows::Forms::Label^ label5;
	private: System::Windows::Forms::Button^ button1;


	protected:

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container^ components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->tb_n = (gcnew System::Windows::Forms::TextBox());
			this->butExample = (gcnew System::Windows::Forms::Button());
			this->dgv = (gcnew System::Windows::Forms::DataGridView());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->tb_epsilon = (gcnew System::Windows::Forms::TextBox());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->butRun = (gcnew System::Windows::Forms::Button());
			this->label5 = (gcnew System::Windows::Forms::Label());
			this->button1 = (gcnew System::Windows::Forms::Button());
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dgv))->BeginInit();
			this->SuspendLayout();
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(12, 9);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(83, 13);
			this->label1->TabIndex = 0;
			this->label1->Text = L"Matrix A ( n x n )";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(12, 35);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(22, 13);
			this->label2->TabIndex = 1;
			this->label2->Text = L"n =";
			// 
			// tb_n
			// 
			this->tb_n->Location = System::Drawing::Point(67, 33);
			this->tb_n->Name = L"tb_n";
			this->tb_n->Size = System::Drawing::Size(100, 20);
			this->tb_n->TabIndex = 2;
			// 
			// butExample
			// 
			this->butExample->Location = System::Drawing::Point(256, 31);
			this->butExample->Name = L"butExample";
			this->butExample->Size = System::Drawing::Size(75, 23);
			this->butExample->TabIndex = 3;
			this->butExample->Text = L"Example";
			this->butExample->UseVisualStyleBackColor = true;
			this->butExample->Click += gcnew System::EventHandler(this, &Form1::butExample_Click);
			// 
			// dgv
			// 
			this->dgv->ColumnHeadersHeightSizeMode = System::Windows::Forms::DataGridViewColumnHeadersHeightSizeMode::AutoSize;
			this->dgv->Location = System::Drawing::Point(15, 120);
			this->dgv->Name = L"dgv";
			this->dgv->Size = System::Drawing::Size(824, 254);
			this->dgv->TabIndex = 4;
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(641, 30);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(35, 13);
			this->label3->TabIndex = 5;
			this->label3->Text = L"label3";
			// 
			// tb_epsilon
			// 
			this->tb_epsilon->Location = System::Drawing::Point(67, 61);
			this->tb_epsilon->Name = L"tb_epsilon";
			this->tb_epsilon->Size = System::Drawing::Size(100, 20);
			this->tb_epsilon->TabIndex = 7;
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Location = System::Drawing::Point(12, 61);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(49, 13);
			this->label4->TabIndex = 6;
			this->label4->Text = L"epsilon =";
			// 
			// butRun
			// 
			this->butRun->Location = System::Drawing::Point(256, 61);
			this->butRun->Name = L"butRun";
			this->butRun->Size = System::Drawing::Size(75, 23);
			this->butRun->TabIndex = 8;
			this->butRun->Text = L"Run";
			this->butRun->UseVisualStyleBackColor = true;
			this->butRun->Click += gcnew System::EventHandler(this, &Form1::butRun_Click);
			// 
			// label5
			// 
			this->label5->AutoSize = true;
			this->label5->Location = System::Drawing::Point(441, 33);
			this->label5->Name = L"label5";
			this->label5->Size = System::Drawing::Size(35, 13);
			this->label5->TabIndex = 9;
			this->label5->Text = L"label5";
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(629, 56);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(75, 23);
			this->button1->TabIndex = 10;
			this->button1->Text = L"button1";
			this->button1->UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(6, 13);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(890, 477);
			this->Controls->Add(this->button1);
			this->Controls->Add(this->label5);
			this->Controls->Add(this->butRun);
			this->Controls->Add(this->tb_epsilon);
			this->Controls->Add(this->label4);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->dgv);
			this->Controls->Add(this->butExample);
			this->Controls->Add(this->tb_n);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->label1);
			this->Name = L"Form1";
			this->Text = L"Eigenvalues & Eigenvectors by Lukas Setiawan";
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^>(this->dgv))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void butExample_Click(System::Object^ sender, System::EventArgs^ e) {


		int i, j, n;
		Double epsilon;
		Double A[101][101];
		Double D[101][101];

		Double A[101][101];

		tb_n->Text = "4";
		tb_epsilon->Text = "1e-06";
		int n = Convert::ToInt32(tb_n->Text);
		epsilon = Convert::ToDouble(tb_epsilon->Text);


		dgv->ColumnCount = 2 * n + 5; //view grid only
		dgv->RowCount = 4 * n + 5;

		//Example Matrix A (4 x 4)
		dgv->Rows[0]->Cells[0]->Value = 8;
		dgv->Rows[0]->Cells[1]->Value = -1;
		dgv->Rows[0]->Cells[2]->Value = 3;
		dgv->Rows[0]->Cells[3]->Value = -1;

		dgv->Rows[1]->Cells[0]->Value = -1;
		dgv->Rows[1]->Cells[1]->Value = 6;
		dgv->Rows[1]->Cells[2]->Value = 2;
		dgv->Rows[1]->Cells[3]->Value = 0;

		dgv->Rows[2]->Cells[0]->Value = 3;
		dgv->Rows[2]->Cells[1]->Value = 2;
		dgv->Rows[2]->Cells[2]->Value = 9;
		dgv->Rows[2]->Cells[3]->Value = 1;

		dgv->Rows[3]->Cells[0]->Value = -1;
		dgv->Rows[3]->Cells[1]->Value = 0;
		dgv->Rows[3]->Cells[2]->Value = 1;
		dgv->Rows[3]->Cells[3]->Value = 7;

		//		for (i = 1;i <= n;i++) {
			//		for (j = 1;j <= n;j++) {
				//		A[1][1].ToString();] = Convert::ToDouble

		//		for (i = 1;i <= n;i++) {
					//(dgv->Rows[0]->Cells[i-1]->Value) = "Col-" + i.ToString();
			//		dgv->Rows[0]->Cells[i - 1]-> = "Col-" + i.ToString();

					//dgvLinear.Rows[i].HeaderCell.Value = "Equation-" + (i + 1).ToString();
					//dgvLinear.Columns[i].HeaderText = "A " + (i + 1).ToString() + " X " + (i + 1).ToString();






		for (i = 1;i <= n;i++) {
			for (j = 1;j <= n;j++) {
				A[i][j] = Convert::ToDouble(dgv->Rows[i - 1]->Cells[j - 1]->Value);
				//D[i][j] = Convert::ToDouble(dgv->Rows[i - 1]->Cells[j - 1]->Value);

			}
		}

		//A[1][1] = Convert::ToDouble(dgv->Rows[1]->Cells[1]->Value); //Cells=column

		label3->Text = A[1][1].ToString();
		label5->Text = "df";



	}




	private: System::Void butRun_Click(System::Object^ sender, System::EventArgs^ e) {

		/*
		int i, j, k, p, q, n, MaxSweep, CountS;
		Double epsilon, Sum, RMS, T, Theta, C, S;
		Double A[101][101];
		Double D[101][101];
		Double V[101][101];
		Double XP[101], XQ[101];
		Double YP[101], YQ[101];

		//Double Theta[101][101];

		//Double diag_diagD[101][101];
		std::string state;

		label5->Text = "ok";

		//tb_n->Text = "4";
		MaxSweep = 50;
		tb_epsilon->Text = "1e-07";

		n = Convert::ToInt32(tb_n->Text);
		epsilon = Convert::ToDouble(tb_epsilon->Text);

		dgv->ColumnCount = 2 * n + 5; //view grid only
		dgv->RowCount = 4 * n + 5;


		//Matrix A
		for (j = 1;j <= n;j++) {
			for (k = 1;k <= n;k++) {
				A[j][k] = Convert::ToDouble(dgv->Rows[j - 1]->Cells[k - 1]->Value);
			}
		}

		//A[1][1] = Convert::ToDouble(dgv->Rows[1]->Cells[1]->Value); //Cells=column

		//label3->Text = A[1][1].ToString();
		//label3->Text = n.ToString();

		//label3->Text = A[2][2].ToString();
		//label3->Text = n.ToString();


		for (j = 1;j <= n;j++) {
			for (k = 1;k <= n;k++) {
				if (j != k)
					V[j][k] = 0;
				else
					V[j][k] = 1;
			}
		}
		CountS = 0;

		do {
			Sum = 0;
			for (j = 1;j <= n;i++) {
				Sum = Sum + A[j][j] * A[j][j];
			}
			RMS = sqrt(Sum / n);
			T = RMS;

			CountS = CountS + 1;
			state = "Done";

			for (p = 1;p <= n - 1;p++) {
				for (q = p + 1;q <= n;q++) {
					if (abs((A[p][q] / T)) > epsilon) {
						Theta = (A[q][q] - A[p][p]) / (2 * A[p][q]);
						T = signed(Theta) / (abs(Theta) + sqrt(Theta * Theta + 1));
						C = 1 / sqrt(T * T + 1);
						S = C * T;

						for (j = 1;j <= n;j++) {
							XP[j] = A[j][p];
							XQ[j] = A[j][q];
						}

						A[p][q] = 0;
						A[q][p] = 0;
						A[p][p] = C * C * XP[p] + S * S * XQ[q] - 2 * C * S * XQ[p];
						A[q][q] = S * S * XP[p] + C * C * XQ[q] + 2 * C * S * XQ[p];

						for (j = 1;j <= n;j++) {

							if ((j != p) && (j != q)) {
								A[j][p] = C * XP[j] - S * XQ[j];
								A[p][j] = A[j][p];
								A[j][q] = C * XQ[j] + S * XP[j];
								A[q][j] = A[j][q];
							}
						}

						for (j = 1;j <= n;j++) {
							YP[j] = V[j][p];
							YQ[j] = V[j][q];
						}

						for (j = 1;j <= n;j++) {
							V[j][p] = C * YP[j];
							YQ[j] = V[j][q] - S * YQ[j];
							V[j][q] = S * YP[j] + C * YQ[j];
						}


					}




				}
				state = "iterating";

				//V[2][2] = Convert::ToDouble(dgv->Rows[1]->Cells[1]->Value);
				//label3->Text= V[2][2].ToString();
			}
		} while (state == "iterating");

		//		for (i = 1;i <= n;i++) {
			//		for (j = 1;j <= n;j++) {
				//		A[1][1].ToString();] = Convert::ToDouble

		//		for (i = 1;i <= n;i++) {
					//(dgv->Rows[0]->Cells[i-1]->Value) = "Col-" + i.ToString();
			//		dgv->Rows[0]->Cells[i - 1]-> = "Col-" + i.ToString();

					//dgvLinear.Rows[i].HeaderCell.Value = "Equation-" + (i + 1).ToString();
					//dgvLinear.Columns[i].HeaderText = "A " + (i + 1).ToString() + " X " + (i + 1).ToString();







		/*
		for (i = 1;i <= n;i++) {
			for (j = 1;j <= n;j++) {
				A[i][j] = Convert::ToDouble(dgv->Rows[i - 1]->Cells[j - 1]->Value);
				D[i][j] = A[i][j];
				//D[i][j] = Convert::ToDouble(dgv->Rows[i - 1]->Cells[j - 1]->Value);

			}
		}

		for (i = 1;i <= n;i++) {
			for (j = 1;j <= n;j++) {
				if (i == j)
					V[i][j] = 1;
				else
					V[i][j] = 0;
			}
		}

		label3->Text = V[2][3].ToString();

		//state = 1;
		//diag(D)
		for (i = 1;i <= n;i++) {
			for (j = 1;j <= n;j++) {

				if (i == j)
					diag_diagD[i][j] = D[i][j];
				else
					diag_diagD[i][j] = 0;

			}
		}

		/*
		[m1 p] = max(abs(D - diag(diag(D))))
			[m2 q] = max(m1)
			p = p(q)


		while (state == 1) {





		}

		//A[1][1] = Convert::ToDouble(dgv->Rows[1]->Cells[1]->Value); //Cells=column


		//label3->Text = A[1][1].ToString();

	}
	*/
	}
	
};












