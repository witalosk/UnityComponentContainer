# UnityComponentContainer

This is a super simple DiContainer that runs in Unity and relies heavily on Unity.  
It is being developed little by little and **has not yet reached a practical level at all.**  
It is intended for use in projects that are completed in a single scene, such as installations, rather than games.
---
Unityで動作する超シンプルなUnityにがっつり依存したDiContainerです。  
ちょっとずつ開発中で、**まだ実用できるレベルには全く達してません。**  
ゲームというよりかはどちらかというとインスタレーション作品など1シーンで完結するプロジェクトでの利用を想定しています。

### Implemented Features / ある機能
- Container that operates within a Scene
- Component can be registered and resolved as Interface, etc.
- OptionalInjection is the default (if a type not registered in the container is requested, the default value is entered)
- Method Injection
- Dependency injection into dynamically generated GameObjects. (IContainer.Instantiate())
---
- Scene内で動作するContainer
- ComponentをInterfaceなどとして登録、解決できる
- OptionalInjectionがデフォルト(コンテナに登録されていない型が要求された場合はデフォルト値が入る)
- メソッドインジェクション
- 動的生成されたGameObjectへの依存性注入 (IContainer.Instantiate())

### Unimplemented feature / ない機能(今のところ)
- Registering General Non-Component Types
- Registration of Factory
- SceneParenting
- Execution functions such as IInitialize
- Constructor Injection, Field Injection, Property Injection  
etc.
---
- Componentではない一般的な型の登録
- Factoryの登録
- SceneParenting
- IInitializeなどの実行機能
- コンストラクタインジェクション、フィールドインジェクション、プロパティインジェクション  
などなど

### Environment / 動作環境
Unity 2021.3.1f1

## Usage / 使い方
1. Create a `SceneContainer` in the scene.
2. Attach `GameObjectRegistrator` or `ComponentsRegistrator` to the component you want to register in the Container.
3. Write a method with `[Inject]` attribute in the code of the injector.
---
1. `SceneContainer` をシーンに作成
2. Containerに登録したいComponentに `GameObjectRegistrator` か `ComponentsRegistrator` をアタッチして設定
3. 注入する側のコードに `[Inject]` 属性を付けたメソッドを記入 

```c#
    public class BarComponent : MonoBehaviour
    {
        [SerializeField]
        private BarComponent _prefab;

        private float _timer = 0f;
        private bool _isInstantiated = false;
        
        private IFooComponent _fooComponent;
        private IContainer _container;
        
        [Inject]
        public void Construct(IFooComponent fooComponent, IContainer container)
        {
            _fooComponent = fooComponent;
            _container = container;
        }

        private void Start()
        {
            Debug.Log(_fooComponent.Test());
        }

        private void Update()
        {
            _timer += Time.deltaTime;
            if (!_isInstantiated && _timer > 5f)
            {
                _container.Instantiate(_prefab);
                _isInstantiated = true;
            }
        }
    }
```


## Contributing
### Branches
- `main` - メインブランチ
- `feature/{機能名}` - 機能ごとの開発ブランチ
- `fix/{バグの内容}` - バグ改修用のブランチ

マージした後のブランチは基本的には削除すること。

### Coding Style
コーディング規則の詳しい内容は[codingstyle.md](codingstyle.md) を参照してください。  

### Commit Message
以下のタグをメッセージの最初に付与すること。
- `[add]` 機能/ファイルの追加
- `[change]` 機能/ファイルの改修
- `[update]` 設定/サブモジュール/プラグイン等のアップデート
- `[fix]` バグの修正
- `[hotfix]` 重大なバグの修正
- `[clean]` リファクタリング/ファイル・ディレクトリ構造の整理
