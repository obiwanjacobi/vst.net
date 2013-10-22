// stdafx.h : include file for standard system include files,
// or project specific include files that are used frequently,
// but are changed infrequently

#pragma once

#define MSTR(x)   #x
#define SHOW_DEFINE(x) printf("%s=%s\n", #x, MSTR(x))

#include "targetver.h"

#include <stdio.h>
#include <tchar.h>

#include <Windows.h>

