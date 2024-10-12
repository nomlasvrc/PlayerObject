# Player Object Example
VRChatで公開されたPersistence機能のPlayer Objectをより使いやすくするためのコード例です。
## 使い方
1. U#スクリプトを作成します。
2. PlayerObjectBase.csを継承し、名前空間を`Nomlas.PlayerObject`にします。
3. `Player Object Manager`コンポーネントの`Player Object Base`に作成したU#スクリプトを指定します。
4. PlayerObject.csに変数を追加します。[詳しくはこちら](#変数の追加)
5. 作成したU#スクリプトを編集します。

### 変数の追加
PlayerObject.csを編集することで追加できます。以下は変数の例です。
```c#:PlayerObject.cs
public string example
{
    get { return _example; }
    set { _example = value; }
}
[UdonSynced] private string _example;
```
お好みでどうぞ。

## 仕様
PlayerObjectBase.csを継承することで使用できる関数や変数です。
### プロパティ
<table><thead><tr><th>名前</th><th>説明</th></tr></thead>
<tr><td>localPlayerObject</td><td>Local PlayerのPlayer Objectを返します。</td></tr>
<tr><td>isConnected</td><td>Local PlayerのPlayer Objectが作成されたかを返します。</td></tr>
<tr><td>isReady</td><td>Local PlayerのPlayer Objectが準備できたかを返します。</td></tr>
</table>

### メソッド
<table><thead><tr><th>名前</th><th>説明</th></tr></thead>
<tr><td>RequestPersistence</td><td>変数を保存します。内部的にはRequestSerializationです。</td></tr>
</table>

### イベント
<table><thead><tr><th>名前</th><th>引数</th><th>説明</th></tr></thead>
<tr><td>OnDataUpdated</td><td>VRCPlayerApi player</td><td>データが更新されたときに呼び出されます。値が変わっていなくても呼び出されることに注意してください。</td></tr>
<tr><td>OnConnected</td><td>-</td><td>Local PlayerのPlayer Objectが作成されたときに呼び出されます。値は同期していない可能性があります。</td></tr>
<tr><td>OnReady</td><td>-</td><td>準備が完了したときに呼び出されます。</td></tr>
</table>

### OnConnectedとOnReadyについて
ほとんどの場合OnReadyが適しています。Connectは一応残してあるだけです。

## 再配布など
MITライセンスに従ってください。