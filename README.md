This is a proof of concept of a small chat application that encrypts messages with AES alias Rijndael (currently only with a fixed block size of 128bit) after negotiating a symmetric key with ECDH (arbitrary curve). Right now messages are encrypted in ECB mode because I was too lazy to implement CBC.

The tools [here](http://people.eku.edu/styere/Encrypt/JS-AES.html) and [there](http://www-cs-students.stanford.edu/~tjw/jsbn/ecdh.html) helped me a lot, I stole major parts of the ECDH stuff from Tom Wu and used Eugene Styer's AES example to debug my code. Thank you!

As this was an assignment for school I was not allowed to use any .NET crypto magic from `System.Security.Cryptography` besides the RNG.
