# MultiThreadAutoRoller
リリースノート<br>
v1.0.0<br>
2020/06/16<br>
- Prism + ReactivePropertyを用いて、ダミーのダイスを回して画面に表示できるようにした
- 同様に、キャラビルドを (まだ固定テキストだが)画面に表示できるようにした

既知の課題<br>
- Modelの処理は一旦シングルコア。ViewModel側で非同期処理を走らせるに留めている
- 一度ダイスロールを回し始めたら終了まで止まらない (CancellationTokenを発行していない)

&copy; Hourier 2020
