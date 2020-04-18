using System;

namespace UniDisposableChecker
{
	/// <summary>
	/// <para>IDisposable.Dispose が呼び出されたかどうかを検知できるクラス</para>
	/// <para>派生クラスのインスタンスがガベージコレクションで回収される時に</para>
	/// <para>デストラクタで Dispose が呼び出されたかどうかを確認します</para>
	/// <para>派生クラスでは Dispose 関数ではなく DoDispose 関数に破棄処理を記述します</para>
	/// </summary>
	public abstract class DisposableChecker : IDisposable
	{
		//================================================================================
		// 変数
		//================================================================================
#if DISABLE_UNI_DISPOSABLE_CHECKER
#else
		private bool m_isDispose; // Dispose が呼び出された場合 true
#endif

		//================================================================================
		// 関数
		//================================================================================
		/// <summary>
		/// 破棄します
		/// </summary>
		public void Dispose()
		{
#if DISABLE_UNI_DISPOSABLE_CHECKER
#else
			m_isDispose = true;
#endif
			DoDispose();
		}
		
#if DISABLE_UNI_DISPOSABLE_CHECKER
#else
		/// <summary>
		/// インスタンスがガベージコレクションで回収される時に呼び出されるデストラクタ
		/// </summary>
		~DisposableChecker()
		{
			// Dispose が呼び出された場合は何もしない
			if ( m_isDispose ) return;

			HandleException();
		}
#endif

		/// <summary>
		/// 派生クラスで破棄処理を記述する関数
		/// </summary>
		protected abstract void DoDispose();

		/// <summary>
		/// <para>Dispose が呼び出されずに、インスタンスが</para>
		/// <para>ガベージコレクションで回収された時の処理を派生クラスで記述する関数</para>
		/// </summary>
		protected abstract void HandleException();
	}
}