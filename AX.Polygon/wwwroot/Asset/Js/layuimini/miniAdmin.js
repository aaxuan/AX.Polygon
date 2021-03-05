/**
 * date:2020/02/27
 * author:Mr.Chung
 * version:2.0
 * description:layuimini 主体框架扩展
 */
layui.define(["jquery", "miniMenu", "element", "miniTab", "miniTheme"], function (exports) {
    var $ = layui.$,
        layer = layui.layer,
        miniMenu = layui.miniMenu,
        miniTheme = layui.miniTheme,
        element = layui.element,
        miniTab = layui.miniTab;

    var miniAdmin = {
        defaultOptions: {
            iniUrl: "/admin/menu/GetUserMenu",    // 初始化接口
            urlHashLocation: true,      // 是否打开hash定位
            bgColorDefault: false,      // 主题默认配置
            multiModule: true,          // 是否开启多模块
            menuChildOpen: false,       // 是否默认展开菜单
            loadingTime: 0,             // 初始化加载时间
            pageAnim: true,             // iframe窗口动画
            maxTabNum: 10,              // 最大的tab打开数量
        },

        /**
         * 后台框架初始化
         * @param options.iniUrl   后台初始化接口地址
         * @param options.urlHashLocation URL地址hash定位
         * @param options.bgColorDefault 默认皮肤
         * @param options.multiModule 是否开启多模块
         * @param options.menuChildOpen 是否展开子菜单
         * @param options.loadingTime 初始化加载时间
         * @param options.pageAnim iframe窗口动画
         * @param options.maxTabNum 最大的tab打开数量
         */
        render: function (options) {
            if (options == null) {
                options = miniAdmin.defaultOptions;
            }

            options.iniUrl = options.iniUrl || null;
            options.urlHashLocation = options.urlHashLocation || false;
            options.bgColorDefault = options.bgColorDefault || 0;
            options.multiModule = options.multiModule || false;
            options.menuChildOpen = options.menuChildOpen || false;
            options.loadingTime = options.loadingTime || 1;
            options.pageAnim = options.pageAnim || false;
            options.maxTabNum = options.maxTabNum || 20;

            let homeInfo = {
                title: "首页",
                href: "/Admin/Home/Welcome"
            };
            let logoInfo = {
                title: "AX.Polygon",
                image: "",
                href: ""
            }

            miniAdmin.getApi(options.iniUrl, null, function (data) {
                console.info(data);
                miniAdmin.renderLogo(logoInfo);
                miniAdmin.renderHome(homeInfo);
                miniAdmin.renderAnim(options.pageAnim);
                miniAdmin.listen();
                miniMenu.render({
                    menuList: data,
                    multiModule: options.multiModule,
                    menuChildOpen: options.menuChildOpen
                });
                miniTab.render({
                    filter: 'layuiminiTab',
                    urlHashLocation: options.urlHashLocation,
                    multiModule: options.multiModule,
                    menuChildOpen: options.menuChildOpen,
                    maxTabNum: options.maxTabNum,
                    menuList: data,
                    homeInfo: homeInfo,
                    listenSwichCallback: function () {
                        miniAdmin.renderDevice();
                    }
                });
                miniTheme.render({
                    bgColorDefault: options.bgColorDefault,
                    listen: true,
                });
                miniAdmin.deleteLoader(options.loadingTime);
            })
        },

        /**
         * 初始化logo
         * @param data
         */
        renderLogo: function (data) {
            //var html = '<a href="' + data.href + '"><img src="' + data.image + '" alt="logo"><h1>' + data.title + '</h1></a>';
            var html = '<a href="' + data.href + '"><h1>' + data.title + '</h1></a>';
            $('.layuimini-logo').html(html);
        },

        /**
         * 初始化首页
         * @param data
         */
        renderHome: function (data) {
            sessionStorage.setItem('layuiminiHomeHref', data.href);
            $('#layuiminiHomeTabId').html('<span class="layuimini-tab-active"></span><span class="disable-close">' + data.title + '</span><i class="layui-icon layui-unselect layui-tab-close">ဆ</i>');
            $('#layuiminiHomeTabId').attr('lay-id', data.href);
            $('#layuiminiHomeTabIframe').html('<iframe width="100%" height="100%" frameborder="no" border="0" marginwidth="0" marginheight="0"  src="' + data.href + '"></iframe>');
        },

        // get 获取 api
        getApi: function (url, parm, callback) {
            console.info(url);
            console.info(window.location.host + url);
            $.ajax({
                url: url,
                type: "get",
                data: parm,
                success: function (obj) {
                    console.info(obj);
                    if (obj.Code === -999) {
                        layer.msg("异常：" + obj.Message);
                        return;
                    }
                    if (obj.Code === 0) {
                        layer.msg("失败：" + obj.Message);
                        return;
                    }
                    if (obj.Code === 1) {
                        layer.msg("成功：" + obj.Message);
                        callback(obj.Data);
                    }
                },
                error: function (xmlhttprequest, errmessage, err) {
                    console.error(xmlhttprequest);
                    console.error(errmessage);
                    console.error(err);
                }
            });
        },

        // get 获取 api
        postApi: function (url, parm, callback) {
            console.info(url);
            console.info(parm);
            console.info(window.location.host + url);
            $.ajax({
                url: url,
                type: "post",
                contentType: "application/json; charset=UTF-8",
                data: JSON.stringify(parm),
                success: function (obj) {
                    console.info(obj);
                    if (obj.Code === -999) {
                        layer.msg("异常：" + obj.Message);
                        return;
                    }
                    if (obj.Code === 0) {
                        layer.msg("失败：" + obj.Message);
                        return;
                    }
                    if (obj.Code === 1) {
                        layer.msg("成功：" + obj.Message);
                        callback(obj.Data);
                    }
                },
                error: function (xmlhttprequest, errmessage, err) {
                    console.error(xmlhttprequest);
                    console.error(errmessage);
                    console.error(err);
                }
            });
        },

        //初始化iframe窗口动画
        renderAnim: function (anim) {
            if (anim) {
                $('#layuimini-bg-color').after('<style id="layuimini-page-anim">' +
                    '.layui-tab-item.layui-show {animation:moveTop 1s;-webkit-animation:moveTop 1s;animation-fill-mode:both;-webkit-animation-fill-mode:both;position:relative;height:100%;-webkit-overflow-scrolling:touch;}\n' +
                    '@keyframes moveTop {0% {opacity:0;-webkit-transform:translateY(30px);-ms-transform:translateY(30px);transform:translateY(30px);}\n' +
                    '    100% {opacity:1;-webkit-transform:translateY(0);-ms-transform:translateY(0);transform:translateY(0);}\n' +
                    '}\n' +
                    '@-o-keyframes moveTop {0% {opacity:0;-webkit-transform:translateY(30px);-ms-transform:translateY(30px);transform:translateY(30px);}\n' +
                    '    100% {opacity:1;-webkit-transform:translateY(0);-ms-transform:translateY(0);transform:translateY(0);}\n' +
                    '}\n' +
                    '@-moz-keyframes moveTop {0% {opacity:0;-webkit-transform:translateY(30px);-ms-transform:translateY(30px);transform:translateY(30px);}\n' +
                    '    100% {opacity:1;-webkit-transform:translateY(0);-ms-transform:translateY(0);transform:translateY(0);}\n' +
                    '}\n' +
                    '@-webkit-keyframes moveTop {0% {opacity:0;-webkit-transform:translateY(30px);-ms-transform:translateY(30px);transform:translateY(30px);}\n' +
                    '    100% {opacity:1;-webkit-transform:translateY(0);-ms-transform:translateY(0);transform:translateY(0);}\n' +
                    '}' +
                    '</style>');
            }
        },

        //全屏
        fullScreen: function () {
            var el = document.documentElement;
            var rfs = el.requestFullScreen || el.webkitRequestFullScreen;
            if (typeof rfs != "undefined" && rfs) {
                rfs.call(el);
            } else if (typeof window.ActiveXObject != "undefined") {
                var wscript = new ActiveXObject("WScript.Shell");
                if (wscript != null) {
                    wscript.SendKeys("{F11}");
                }
            } else if (el.msRequestFullscreen) {
                el.msRequestFullscreen();
            } else if (el.oRequestFullscreen) {
                el.oRequestFullscreen();
            } else if (el.webkitRequestFullscreen) {
                el.webkitRequestFullscreen();
            } else if (el.mozRequestFullScreen) {
                el.mozRequestFullScreen();
            } else {
                miniAdmin.error('浏览器不支持全屏调用！');
            }
        },

        //退出全屏
        exitFullScreen: function () {
            var el = document;
            var cfs = el.cancelFullScreen || el.webkitCancelFullScreen || el.exitFullScreen;
            if (typeof cfs != "undefined" && cfs) {
                cfs.call(el);
            } else if (typeof window.ActiveXObject != "undefined") {
                var wscript = new ActiveXObject("WScript.Shell");
                if (wscript != null) {
                    wscript.SendKeys("{F11}");
                }
            } else if (el.msExitFullscreen) {
                el.msExitFullscreen();
            } else if (el.oRequestFullscreen) {
                el.oCancelFullScreen();
            } else if (el.mozCancelFullScreen) {
                el.mozCancelFullScreen();
            } else if (el.webkitCancelFullScreen) {
                el.webkitCancelFullScreen();
            } else {
                miniAdmin.error('浏览器不支持全屏调用！');
            }
        },

        //初始化设备端
        renderDevice: function () {
            if (miniAdmin.checkMobile()) {
                $('.layuimini-tool i').attr('data-side-fold', 1);
                $('.layuimini-tool i').attr('class', 'fa fa-outdent');
                $('.layui-layout-body').removeClass('layuimini-mini');
                $('.layui-layout-body').addClass('layuimini-all');
            }
        },

        //初始化加载时间
        deleteLoader: function (loadingTime) {
            setTimeout(function () {
                $('.layuimini-loader').fadeOut();
            }, loadingTime * 1000)
        },

        ///成功
        success: function (title) {
            return layer.msg(title, { icon: 1, shade: this.shade, scrollbar: false, time: 2000, shadeClose: true });
        },

        //失败
        error: function (title) {
            return layer.msg(title, { icon: 2, shade: this.shade, scrollbar: false, time: 3000, shadeClose: true });
        },

        //判断是否为手机
        checkMobile: function () {
            var ua = navigator.userAgent.toLocaleLowerCase();
            var pf = navigator.platform.toLocaleLowerCase();
            var isAndroid = (/android/i).test(ua) || ((/iPhone|iPod|iPad/i).test(ua) && (/linux/i).test(pf))
                || (/ucweb.*linux/i.test(ua));
            var isIOS = (/iPhone|iPod|iPad/i).test(ua) && !isAndroid;
            var isWinPhone = (/Windows Phone|ZuneWP7/i).test(ua);
            var clientWidth = document.documentElement.clientWidth;
            if (!isAndroid && !isIOS && !isWinPhone && clientWidth > 1024) {
                return false;
            } else {
                return true;
            }
        },

        //监听
        listen: function () {
            /**
             * 刷新
             */
            $('body').on('click', '[data-refresh]', function () {
                $(".layui-tab-item.layui-show").find("iframe")[0].contentWindow.location.reload();
                miniAdmin.success('刷新成功');
            });

            /**
             * 监听提示信息
             */
            $("body").on("mouseenter", ".layui-nav-tree .menu-li", function () {
                if (miniAdmin.checkMobile()) {
                    return false;
                }
                var classInfo = $(this).attr('class'),
                    tips = $(this).prop("innerHTML"),
                    isShow = $('.layuimini-tool i').attr('data-side-fold');
                if (isShow == 0 && tips) {
                    tips = "<ul class='layuimini-menu-left-zoom layui-nav layui-nav-tree layui-this'><li class='layui-nav-item layui-nav-itemed'>" + tips + "</li></ul>";
                    window.openTips = layer.tips(tips, $(this), {
                        tips: [2, '#2f4056'],
                        time: 300000,
                        skin: "popup-tips",
                        success: function (el) {
                            var left = $(el).position().left - 10;
                            $(el).css({ left: left });
                            element.render();
                        }
                    });
                }
            });

            $("body").on("mouseleave", ".popup-tips", function () {
                if (miniAdmin.checkMobile()) {
                    return false;
                }
                var isShow = $('.layuimini-tool i').attr('data-side-fold');
                if (isShow == 0) {
                    try {
                        layer.close(window.openTips);
                    } catch (e) {
                        console.log(e.message);
                    }
                }
            });

            /**
             * 全屏
             */
            $('body').on('click', '[data-check-screen]', function () {
                var check = $(this).attr('data-check-screen');
                if (check == 'full') {
                    miniAdmin.fullScreen();
                    $(this).attr('data-check-screen', 'exit');
                    $(this).html('<i class="fa fa-compress"></i>');
                } else {
                    miniAdmin.exitFullScreen();
                    $(this).attr('data-check-screen', 'full');
                    $(this).html('<i class="fa fa-arrows-alt"></i>');
                }
            });

            /**
             * 点击遮罩层
             */
            $('body').on('click', '.layuimini-make', function () {
                miniAdmin.renderDevice();
            });
        }
    };

    exports("miniAdmin", miniAdmin);
});