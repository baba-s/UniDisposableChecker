# Uni Disposable Checker

* IDisposable.Dispose が呼び出されたかどうかを検知できるクラス
* IDisposable.Dispose が呼び出されていない場合は、  
インスタンスがガベージコレクションで回収される時に HandleException 関数が呼び出される  
* 派生クラスでは Dispose 関数ではなく DoDispose 関数に破棄処理を記述する  
* `DISABLE_UNI_DISPOSABLE_CHECKER` シンボルを定義すると、解放チェックを無効化できる  

## 使用例

```cs
using UniDisposableChecker;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class TestClass : DisposableChecker
{
	protected override void DoDispose()
	{
	}

	protected override void HandleException()
	{
		Debug.Log( "解放漏れ" );
	}
}

public class Example : MonoBehaviour
{
	private readonly TestClass m_test = new TestClass();

	private void Update()
	{
		if ( Input.GetKeyDown( KeyCode.Space ) )
		{
			var activeScene = SceneManager.GetActiveScene();
			var buildIndex  = activeScene.buildIndex;

			SceneManager.LoadScene( buildIndex );
		}
	}
}
```