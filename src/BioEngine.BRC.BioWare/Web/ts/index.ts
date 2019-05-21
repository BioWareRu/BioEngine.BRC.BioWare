require('bootstrap/dist/js/bootstrap');
require('mdbootstrap/js/mdb');
require('mdbootstrap/js/addons/masonry.pkgd.min');
require('mdbootstrap/js/addons/imagesloaded.pkgd.min');
require('wow.js/dist/wow');
import WOW = require('wow.js/dist/wow');

new WOW().init();

require('./blocks/gallery');
require('./blocks/youtube');
require('./blocks/twitter');
require('./blocks/twitch');


