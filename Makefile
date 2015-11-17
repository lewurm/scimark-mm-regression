SHELL := bash

all: slow.exe fast.exe

slow.exe: hax.cs
	mcs -d:SLOW $<
	mv hax.exe $@

fast.exe: hax.cs
	mcs -d:FAST $<
	mv hax.exe $@
