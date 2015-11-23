SHELL := bash

all: slow.exe fast.exe woot

slow.exe: hax.cs
	mcs -d:SLOW $<
	mv hax.exe $@

fast.exe: hax.cs
	mcs -d:FAST $<
	mv hax.exe $@

woot woot.s: woot.c Makefile
	gcc -Wall -Werror -O1 $< -o woot
	gcc -Wall -Werror -O1 -S $<
