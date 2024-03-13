# Delete all 'bin' and 'obj' folders within LedgerscopeSolution folder.

$solutionDir = Join-Path -Path (Get-Item $PSScriptRoot).Parent.Parent -ChildPath "Ledgerscope.*"
Write-Output "Deleting bin, obj, and similar from $($solutionDir)"
Write-Output "Working on it..."
Get-ChildItem $solutionDir -include 'bin', 'obj', 'bin.core', 'obj.core' -Recurse | ForEach-Object ($_) { Remove-Item $_.FullName -Force -Recurse }
Write-Output "Done."