# KUBC.DAYZ.GAME
Инструменты для работы с серверными файлами DAYZ SA. 
[Описание на сайте разработчика](https://kubcoder.ru/dayzgame/About)
# Изменения
## 8.0.1
### Событие suicide
Исправлен баг расшифровки события суицида, когда игра по каким то причинам не добавляет координату произошедшего и при расшифровке к идентификатору игрока приляпывалась скобочка в конце.
Т.е. вот такие строчки вносили ошибку
```
16:13:54 | Player 'qstarnik' (id=OIFC5TG5SbUbANAbOuMOCRAKGo-A9aAqCe-33pCyc8U=) committed suicide.
```
### Список игроков
Добавили в элемент журнала игроков отметку что игрок мертв, т.е. учет слова (DEAD) в списке игроков
```
10:16:24 | ##### PlayerList log: 2 players
10:16:24 | Player "Survivor" (DEAD) (id=ixg8qbLRPHnpN15b2sSbYYiMzdbVa2r43l07ZBxhAqw= pos=<12766.9, 10048.1, 5.6>)
10:16:24 | Player "Yappietouch" (id=zirYiNHKZT2d5K7rusVdbSWNzF6AUa2PQFSg9EuYkGE= pos=<1619.3, 5010.7, 172.1>)
10:16:24 | #####
```