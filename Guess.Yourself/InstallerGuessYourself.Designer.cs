namespace Guess.Yourself
{
    partial class Lite
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            // 
            // Lite
            // 
            this.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.УстановитьУгадайСебяLite_AfterInstall);
            this.AfterUninstall += new System.Configuration.Install.InstallEventHandler(this.УстановитьУгадайСебяLite_AfterUninstall);
            this.BeforeInstall += new System.Configuration.Install.InstallEventHandler(this.InstallGuessYourself_BeforeInstall);

        }

        #endregion
    }
}