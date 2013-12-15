%%% ОБЫЧНЫЕ СПИСКИ — обычно пердставление %%%
% длина списка
llen([], 0). %условие выхода из рекурсии. факт что пустой список длина
% равна 0. тоже самое llen([], 0) :- true.

% отделяем голову в аннонимную переменую и запускаем рекурсивно
% разделение хвоста пока не получим факт что пустой список равен 0 на
% каждом шаге добавляем одик в длине
llen([_|T], L) :-
	llen(T, L1),
	L is L1 + 1.

%член списка
lmem(H, [H|_]). %факт H равно голове списка

% отделяем хвотс и проверяем истиность выше указаного факта
lmem(I, [_|T]) :-
	lmem(I, T).

% слияние, деление и проверка эдентичности списков в общем предикарт
% append
lconc([], L, L) % факт пустой список добавленый к списку L дает список L
:- write('[]'), write('='), write(''), write('|'), write(''),
write(':'), write(L), nl.

lconc([H|T], L, [H|R]) :-
	lconc(T, L, R),
       write([H|T]), write('='), write(H), write('|'), write(T), write(':'),
       write(R), nl.


% надо это понять
% допустим имеем: [1, 2, 3], [5, 6], Res
% 1. H = 1, T = [2, 3], L = [5, 6], Res = [1|[]]
% 2. H = 2, T = [3], L = [5, 6], Res = [1|[2|[]]]
% 3. H = 3, T = [], L = [5, 6], Res = [1|[2|[3|[]]]]
% 4. [], L = [5, 6], Res = [1|[2|[3|[5, 6]]]]]
% только правда Res формируется в обратном порядке

% удаление элемента I из списка.
% select/3
ldel(I, [I|T], T). %факт если I голова то вернуть хвотс.

ldel(I, [H|T], [H|R]) :-
	ldel(I, T, R).

% получить N-ый элемент
lget_n([I|_], 0, I) :- !.

lget_n([_|L], N, I):-
	N1 is N - 1,
	lget_n(L, N1, I).

% поиск подсписка
lsubl([], []) :- !.

lsubl([H|T], [H|R]) :-
	lsubl(T, R).
lsubl([_|T], R) :-
	lsubl(T, R).

% перестановка
lpm([], []).

lpm([H|T], R) :-
	lpm(T, R1),
	ldel(H, R, R1).

lpm2([], []).
lpm2(L, [H|T]) :-
	ldel(H, L, R),
	lpm2(R, T).


% === Задача вариант 6 ===

%удаление N первых элементов.
ldel_n([], _, []) :- !. %если список пуст факт что при любом N он будет пуст, делаем отсечение.

ldel_n([_|L], 1, L) :- !. %как при N равном 1 выдаем хвост и отсекаем.

ldel_n([_|T], N, R) :-
	N1 is N - 1,
	ldel_n(T, N1, R).

ldel_n_b(L, N, R) :-
	append(X, R, L),
	(N > 0 -> length(X, N); R = L), !.


%%% ОБЫЧНЫЕ СПИСКИ — порядковое представление %%%
% элементами списка являются функторы
% e(i, val)
% e(i, j, val)
% по сутит это теже матрици что в с++
% [1, 4, 7, 10] = [e(1, 1), e(2, 4), e(3, 7), e(4, 10)]
% [[1, 2], [3, 4]] = [e(1, 1, 1), e(1, 2, 2), e(2, 1, 3), e(2, 2, 4)]
% для них подходят описанные выше придекарты.
ldel_n_ord([], _, []).
ldel_n_ord([e(N,_)|L], N, L):-!.
ldel_n_ord([_|L], N, R):-
%	write(N1), write(':'), write(L), write('N:'), write(N), nl,
	ldel_n_ord(L, N, R).


ldel_n_ord_ns(L, N, R) :-
	ldel_n_ord_ns(L, N, [], R).
ldel_n_ord_ns([], _, R, R).
ldel_n_ord_ns([e(X, V)|L], N, T, R):-
%	write(X), write(':'), write(L), write('T:'), write(T), nl,
	X > N -> ldel_n_ord_ns(L, N, [e(X, V)|T], R); ldel_n_ord_ns(L, N, T,
	R).

%%% Числовые списки %%%
%вычисление числа четных элементов
getCountEven(L, R):-
	getCountEven(L, 0, R).
getCountEven([], S, S).
getCountEven([H|T], S, R):-
%	write(H), write(':'), write(S), nl,
	(number(H),
	 H1 is H / 2,
	 H2 is H1 * 2,
	 H2 = H ->
	 S1 is S + 1; S1 is S),
	getCountEven(T, S1, R).

getCountEven_ord(L, R):-
	getCountEven_ord(L, 0, R).
getCountEven_ord([], S, S).
getCountEven_ord([e(_, H)|T], S, R):-
	write(H), write(':'), write(S), nl,
	(number(H),
	 H1 is H / 2,
	 H2 is H1 * 2,
	 H2 = H ->
	 S1 is S + 1; S1 is S),
	getCountEven_ord(T, S1, R).

w(Obj):-write(Obj).
wl(Obj):-write(Obj), nl.
wp(Obj):-write(Obj), write(':').

:- wl('TEST')
	, wp('llen([], L)'), llen([], L), wl(L)
	, wp('llen([1, 2, 3], L)'), llen([1, 2, 3], L1), wl(L1)
	, wp('lmem(2, [1, 2, 3])'), lmem(2, [1, 2, 3]), wl('true')
	, wp('lconc([1, 2], [3, 4], L)'), lconc([1, 2], [3, 4], L3), wl(L3)
	.



