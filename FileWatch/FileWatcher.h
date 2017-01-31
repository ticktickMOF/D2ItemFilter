#pragma once

#include "api.h"
#include <string>
#include <Windows.h>
#include <functional>

class FileWatcherImpl;

class FILEWATCH_API FileWatcher {
public:
	FileWatcher(const std::wstring directory, const std::wstring filename, std::function<void(void)> callback);
	~FileWatcher();
	void WatchFile();
private:
	FileWatcherImpl *_pimpl;
};

