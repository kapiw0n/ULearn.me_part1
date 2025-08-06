public static List<DirectoryInfo> GetAlbums(List<FileInfo> files)
{
	var dirs = new List<DirectoryInfo>();
	var matches = files.Where(x => x.FullName.EndsWith(".mp3") || x.FullName.EndsWith(".wav"));
	foreach (var match in matches)
	{
		if (!dirs.Exists(dir => dir.FullName == match.Directory.FullName))
			dirs.Add(match.Directory);
	}
	return dirs;
}