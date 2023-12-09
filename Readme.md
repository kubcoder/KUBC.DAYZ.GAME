# KUBC.DAYZ.GAME
Инструменты для работы с серверными файлами DAYZ SA. 
[Описание на сайте разработчика](https://kubcoder.ru/dayzgame/About)

# Изменения
## 8.2.x

Инструменты работы с файлом types.xml (KUBC.DAYZ.GAME.MissionFiles.DB.Types)
- Добавлен инструментарий отслеживания изменений элемента файла
- Исправлен алгоритм сохранения в XML, исключили запись значений null

## 8.1.3
И тут мы нашли ошибочку... гребанные игроки юзают круглые скобачки в именах, а это крашит...

Так же найдено что иногда бывает указание идентфикатора как `(id=Unknown)`

Для исключения данных проблем исправили чтение всех логов

## 8.1.2
> **логи наше все!!!**
## Процедура чтения логов
В функцию чтения добавлена возможность передачи токена отмены, для прерывания процесса в штатном режиме.
Кроме того добавили проверку на окончание строки, т.е. если прочитанная строка не завершена игровым сервером то её парсинг не будет осуществлятся. Дочитать строку можно повторным
вызовом соответсвующего метода.
## События
Выполнили полный рефакторинг всех читаемых событий:
- выполнили рефакторинг кода, вынесли часто используемые функции в базовые классы. проверили все места где могло произойти зацикливание, и убедились что бесконечные циклы не возникают ни при каких условиях.
- выполнили исправление "незначительных отклонений" в записе логов что приводило к фатальным ошибкам в работе парсеров

[Мы обнаружили](https://kubcoder.ru/news/dayz/admLog) что в журнале администратора (*.ADM) появились новые события и добавили их:
- **Lowered** опускание тотема. Т.е. событие когда игрок опускает некий флаг на неком флагштоке. Судя по движку кроме флагштоков могут быть и дргуие тотемы. Поэтому в событии мы указываем не только какой флаг опустили но и тип тотема.
- **Raised** поднятие тотема. Т.е. событие когда игрок поднимает некий флаг на неком флагштоке. Судя по движку кроме флагштоков могут быть и дргуие тотемы. Поэтому в событии мы указываем не только какой флаг подняли но и тип тотема.
- **Folded** сворачивание размещенного предмета. Например разметки забора и т.д., возможно работает на ловушках для рыбы.
- **Paked** событие упаковки палатки для переноски.
- **Mounted** событие прикручивания навесных элементов на элементы построенных конструкций.
- **Unmounted** событие откручивания навесных элементов с построенных конструкций.
- **DugIn** событие закапывания схрона.
- **DugOut** событие выкапывания схрона.

## [8.1.0](https://github.com/kubcoder/KUBC.DAYZ.GAME/releases/tag/8.1.0)
### Исправлено чтение логов
Добавлен функционал что после чтения файл лога не закрывается самостоятельно, что позволяет дочитывать файл время от времени.
Ожидаем что это повысит производительность службы игровых экземпляров при перечитывании логов на ходу работы сервера.

## [8.0.1](https://github.com/kubcoder/KUBC.DAYZ.GAME/releases/tag/8.0.1)
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
### Исправлена работа с cfgeffectarea.json
В класс зоны заражения не была добавлена секция **PlayerData**

### Обновлен интерфейс файла cfggameplay.json
Вкатили все изменения в файле до версии 1.22