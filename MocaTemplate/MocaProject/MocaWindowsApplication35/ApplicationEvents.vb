Imports Moca.Di

Namespace My

	''' <summary>
	''' MyApplication
	''' </summary>
	''' <remarks>
	''' ���̃C�x���g�� MyApplication �ɑ΂��ė��p�ł��܂�:<br/>
	''' <br/>
	''' Startup: �A�v���P�[�V�������J�n���ꂽ�Ƃ��A�X�^�[�g�A�b�v �t�H�[�����쐬�����O�ɔ������܂��B<br/>
	''' Shutdown: �A�v���P�[�V���� �t�H�[�������ׂĕ���ꂽ��ɔ������܂��B���̃C�x���g�́A�ʏ�̏I���ȊO�̕��@�ŃA�v���P�[�V�������I�����ꂽ�Ƃ��ɂ͔������܂���B<br/>
	''' UnhandledException: �n���h������Ă��Ȃ���O���A�v���P�[�V�����Ŕ��������Ƃ��ɔ�������C�x���g�ł��B<br/>
	''' StartupNextInstance: �P��C���X�^���X �A�v���P�[�V�������N������A���ꂪ���ɃA�N�e�B�u�ł���Ƃ��ɔ������܂��B <br/>
	''' NetworkAvailabilityChanged: �l�b�g���[�N�ڑ����ڑ����ꂽ�Ƃ��A�܂��͐ؒf���ꂽ�Ƃ��ɔ������܂��B<br/>
	''' </remarks>
	Partial Friend Class MyApplication

		Private Sub MyApplication_Startup(sender As Object, e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup
			' Moca������
			MocaContainerFactory.Init()
		End Sub

		Private Sub MyApplication_Shutdown(sender As Object, e As System.EventArgs) Handles Me.Shutdown
			' Moca������
			MocaContainerFactory.Destroy()
		End Sub

	End Class


End Namespace

