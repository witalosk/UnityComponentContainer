# このプロジェクトのC#コーディング規約
このスタイルガイドはこのプロジェクトで使用するC#用のスタイルガイドです。HLSLファイルは必ずしもこれに準拠しません。もともとは Google 社内で開発された C# コード用であり、Google の C# コードのデフォルトのスタイルです。 元のものより規則を少なくしたり、一部規則を変更したりしています。
## フォーマットについての規則
### 命名規則
命名規則は Microsoft の C# 命名ガイドラインに従います。Microsoft の命名ガイドラインが指定されていない場合（プライベート変数やローカル変数など）、ルールは CoreFX C# コーディングガイドラインから取得されます。
ルールの概要:
#### コード
- クラス、メソッド、列挙型、パブリックフィールド、パブリックプロパティ、名前空間の名前：PascalCase
- ローカル変数の名前、パラメーター：camelCase
- private、protected、internal および protected な internal フィールドとプロパティの名前：_camelCase。
- 命名規則は、const、static、readonlyなどの修飾子の影響を受けません。
- 頭文字を含め、スペースがないひとまとまりの文字列をひとつの「単語」として扱います。これによってケーシングの行い方が決定されます。たとえば "My SPH" は MySPH ではなく MySph です。
- インターフェースの名前は I で始まります。例：ITrailLine
- 基底クラスの名前はBaseで終わります。例：MyTrailLineBase
#### ファイル
- ファイル名とディレクトリ名はPascalCaseです。例：MyFile.cs
- 可能な場合、ファイル名はファイル内のメインクラスの名前と同じにする必要があります。例：MyClass.cs
- 一般に、ファイルごとに1つのコアクラスを優先します。
### グループと整理
- 修飾子は次の順序で発生します：`public protected internal private new abstract override sealed static readonly extern unsafe volatile async`
- 名前空間の `using` 宣言は、これから定義する名前空間の宣言より前の上部に配置します。
- クラスメンバーの順序は必ずしも意識しなくて良いですが、Publicなものが上の方に来るようにしてください。Debug用のメソッドは下の方に配置してください。
- private修飾子は省略せず、可能な限り記載してください。Unityのイベント関数も同様です。
### スペース（空白文字、改行、インデント）についてのルール
Google Java スタイルにもとづきます。一部改変しています。
- 1行あたり最大1つのステートメント。
- ステートメントごとに最大1つの割り当て。
- 4つのスペースのインデント、タブなし。
- 1行の最大文字数は100文字。
- namespaceと、クラス、構造体、列挙体などのオブジェクトの定義には、波括弧の開始前に改行する。
- if文、switch文、while文などの文法の波括弧の開始前にも改行を入れる。
- 波括弧閉じと `else` の間は改行する。
- `if`/`for`/`while` 等やカンマのあとにはスペースを挿入する。
- (括弧開きの直後、または)括弧閉じの直前にはスペースを入れる。
- 単項演算子とそのオペランドの間にスペースは入れません。演算子と他のすべての演算子の各オペランドの間の1つのスペースを入れる。
### コード例
```c#
using System;                                           // `using` は名前空間よりも前に書く。
namespace MyNamespace
{                                                       // 名前空間は PascalCase、波括弧で改行。
    public interface IMyInterface                       // インターフェース名は 'I' で開始。波括弧で改行。
    {
        public int Calculate(float value, float exp);   // メソッド名は PascalCase。
    }
  
    public enum MyEnum                                  // 列挙型名は PascalCase。
    {                              
        Yes,                                            // 列挙子名も PascalCase。
        No,
    }
  
    public class MyClass                                // クラス名は PascalCase。波括弧で改行。
    {
        public int Foo = 0;                             // パブリックメンバー名は PascalCase。
        public bool NoCounting = false;                 // フィールドの初期化を奨励。
        
        private class Results
        {
            public int NumNegativeResults = 0;
            public int NumPositiveResults = 0;
        }
        
        private Results _results;                       // プライベートメンバー名は _camelCase 。
        public static int NumTimesCalled = 0;
        private const int _bar = 100;                   // static/const 修飾子は命名規則に影響しない。
        
        public int CalculateValue(int mulNumber)        // メソッドの定義の括弧開き前は改行。
        {
            var resultValue = Foo * mulNumber;          // ローカル変数名は camelCase。
            NumTimesCalled++;
            Foo += _bar;
            
            if (!NoCounting)
            {                                           // 'if' のあとにはスペースを入れる。各括弧()の内側にはスペースを入れない。
                if (resultValue < 0) 
                {                                       // 比較演算子の前後にスペースを入れる。
                    _results.NumNegativeResults++;
                }
                else if (resultValue > 0) 
                {                                       // 波括弧と else の間は改行。
                    _results.NumPositiveResults++;
                }
            }
            
            return resultValue;
        }
      
        void DoNothing() {}                               // 空のブロックは簡潔に。
    }
}
```
## C# コーディングガイドライン
ここからの内容は強制ではなく推奨です。
### 定数
- `const` にすることができる変数とフィールドは、常に `const` にする必要があります。
- `const` が不可能な場合は `readonly` が適切な代替手段になります。
- マジックナンバーよりも名前付き定数を優先します。
### プロパティのスタイル
- 単一行の読み取り専用プロパティの場合、可能な場合は式形式 (`=>`) のプロパティを優先します。
- それ以外の場合は昔ながらの `{ get; set; }` 記法を使用します。
### 式形式構文
以下のような式があるとします。
```c#
int SomeProperty => _someProperty
```
- ラムダ式とプロパティにおいて、式形式構文は慎重に使用しましょう。
- メソッドの定義には使用しないでください。
- メソッドやその他のスコープブロックと同様に、中括弧開きを含む行の最初の文字に終了を揃えます。例についてはサンプルコードを参照してください。
### ラムダ式 vs 名前付きメソッド
- そのラムダ式が自明でない場合 (例えば、宣言を除いて2つ以上のステートメントを含む場合など)、またはいくつかの場所で繰り返し使用される場合は名前付きのメソッドを使用するべきです。
### フィールドの初期化
- 通常、フィールドの初期化は奨励されます。
### Utilityクラス
- 繰り返し使用するstaticな処理がある場合はUtilityクラスに外出ししてください。
- その場合、適切な粒度でUtilityクラスを分割してください。
### LINQ
- 一般に、LINQ の長いチェーンではなく単一行の LINQ 呼び出しと命令型のコードを優先します。命令型のコードと高度にチェーンされた LINQ を混在させると可読性が落ちます。
- SQL スタイルの LINQ キーワードよりもメンバー拡張メソッドを優先します。例えば `myList where x` 形式よりも `myList.Where(x)` 形式の使用を優先します。
- `Container.ForEach(...)` が単一のステートメントよりも長くなる場合は使用を避けてください。
### 配列 vs リスト
- 通常、public な変数やプロパティ、戻り値の型には配列よりも `List<>` の使用を優先します。
- コンテナのサイズが変更される可能性がある場合は `List<>` の使用を優先します。
- コンテナのサイズが固定されており、構築時にわかっている場合は配列を優先します。
- 多次元配列には配列を優先します。
- 注意事項
    - array と `List<>` は、どちらも線形で連続したコンテナを表現します。
    - C++ における配列と `std::vector` と同様に、配列は固定長で、`List<>` は追加が可能です。
    - 配列のパフォーマンスの方が高い場合もありますが、一般的には `List<>` の方が柔軟性があります。
### フォルダとファイルの配置
- プロジェクト内で一貫性を保ちましょう。
### 名前空間のネーミング
- 基本的にはファイル/フォルダのレイアウトを名前空間と一致させてください。
- 深すぎるネストは避けてください。(基本は2で、3が限度)
- 名前空間の一番はじめはプロジェクト名としてください。
  例：プロジェクト"Waterfall"の Assets/Scripts/TrailLine/TrailLineController.csにおける名前空間
  `namespace Waterfall/TrailLine`
### コメント
- 基本的にはコメントより、変数やメソッドの命名で役割を伝えることを意識してください。
- publicなメソッドや基底クラスのprotectedなメソッドなど、そのクラスの外から参照される可能性のあるメソッドやプロパティにはXMLコメントを付加してください。
- それ以外のメソッドについてはXMLコメントは必ずしも必要ではありません。
- Interfaceを噛ませる場合などには、InterfaceにXMLコメントを付加することを推奨します。
### コンテナ反復中のアイテムの削除
C# は他の多くの言語と同様に、反復中にコンテナからアイテムを削除するための明確なメカニズムを提供しません。これにはいくつかのオプションがあります。
- ある条件を満たすアイテムを削除するだけでよい場合は `someList.RemoveAll(somePredicate)` をお勧めします。
- 反復で他の作業を行う必要がある場合は `RemoveAll` では不十分な場合があります。一般的な代替パターンは、ループの外側に新しいコンテナを作成し、その新しいコンテナに保持するアイテムを挿入し、反復の最後に元のコンテナを新しいコンテナと入れ替えるというアプローチです。
### `var` キーワード
- 型名が長かったり、明白であったり、またはそれほど重要でない型名の記述を回避することで可読性を向上させられる場合は `var` の使用をお勧めします。
- 以下のような例では `var` の使用が推奨されます。
    - `var apple = new Apple();` や `var request = Factory.Create<HttpRequest>();` のように型が明白なとき。
- また、以下のような例では非推奨です。
    - 基本的な型を使用するとき。例： `var success = true;`
    - コンパイラーによって解決される組み込みの数値型を使用するとき。例： `var number = 12 * ReturnsFloat();`
    - ユーザーが型を知ることで明らかにメリットが得られる場合。例： `var listOfItems = GetList();`
### 属性
- 属性は関連付けられているフィールド、プロパティ、またはメソッドの上の行に、改行で区切られて表示される必要があります。
- 属性はXMLコメントよりも下に書くことを推奨します。
  例：
```c#
/// <summary>
/// 描画するトレイルの数
/// </summary>
[SerializeField]
private int _currentTrailCount;
```