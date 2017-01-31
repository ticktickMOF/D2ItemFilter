#pragma once
#ifdef FILEWATCH_EXPORT
#define FILEWATCH_API __declspec(dllexport)
#else
#ifdef FILEWATCH_IMPORT
#define FILEWATCH_API __declspec(dllimport)
#else
#define FILEWATCH_API
#endif
#endif