

Game về cơ bản :
Setting giới tính sẽ ở mainmenu 

Player - Skil 1 sẽ ấn hold nút cho đến khi nhân vật phát sáng
	Skill 2 sẽ gọi ra một quả bomb phát nổ sau một thời gian(unlock lv2)
	skill 3 sẽ gọi ra một hố đen hút quái xung quanh vào (lv3)
	
Enemy - Spawner sẽ random spawn ra hai loại quái tầm gần và xa mỗi đợt 10 con cách 30s, lv của quái sẽ scale theo theo lv của nhân vật
(có thể tuỳ chỉnh scale ví dụ như player 3 quái sẽ tăng lv1 hiện tại đang 1:1), lv của quái sẽ như sau
ví dụ lv của "quái" là lv 3 thì sẽ random 1-3 trong đó lv1 10% lv20% lv 70% cứ thế ở lv cao.


Save sẽ lưu lại quá trình của nhân vật (khi restart sẽ chơi từ đầu)

Về stat lv up của player và enemy chỉnh sửa trong file Design.xls sau đó export json lưu vào bên trong thư mục streamingasset
Các chỉ số liên quan như scale, charge, CD sẽ được đính trong prefab của module đó ví dụ 
bomb sẽ do prefab bomb chứa các thông số đó (atk của module sẽ scale theo atk của player)