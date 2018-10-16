import * as webpack from 'webpack';
import * as path from 'path';
import * as HtmlWebpackPlugin from 'html-webpack-plugin';
import * as UglifyJsPlugin from 'uglifyjs-webpack-plugin';

const CleanWebpackPlugin = require('clean-webpack-plugin')

const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const autoprefixer = require('autoprefixer');
import * as fs from 'fs';

const srcPath = path.join(__dirname, '/Web'),
    distPath = path.join(__dirname, '/wwwroot');

const isDevelopment = false;

// This defines all the bundles that get generated, including the .cshtml files
// that get generated so we can include assets in views.
const bundles = fs.readdirSync(path.join(__dirname, '/Web/js/bundles')).map((value) => value.substring(0, value.length - 3));

const config: webpack.Configuration = {
    cache: true,
    devtool: 'source-map',
    context: srcPath,
    entry: {
        'polyfills': [
            'core-js',
        ],
        ...bundles.reduce((map, value) => {
            // builds a JS hashmap like {'page-login': 'js/page-login.ts', 'page-default': 'js/page-default.ts', ...}
            map[value] = [
                './js/bundles/' + value + '.ts',
            ];
            return map;
        }, {})
    },
    output: {
        path: distPath,
        filename: 'js/[name].[chunkhash].' + (isDevelopment ? 'dev' : 'min') + '.js',
        publicPath: '/',
    },
    resolve: {
        extensions: ['.ts', '.tsx', '.js'],
        modules: ["node_modules"],
    },
    module: {
        rules: [
            {
                test: /\.tsx?$/,
                use: [
                    {
                        loader: 'awesome-typescript-loader',
                        options: {
                            configFile: path.join(__dirname, '/tsconfig.webpack.json'),
                            silent: true,
                        }
                    }
                ]
            },
            {
                test: /\.(less|css)/,
                use: [
                    {
                        loader: MiniCssExtractPlugin.loader
                    },
                    {
                        loader: "css-loader",
                    },
                    {
                        loader: 'postcss-loader',
                        options: {
                            plugins: () => autoprefixer({
                                browsers: ['last 3 versions', '> 1%']
                            })
                        }
                    },
                    {
                        loader: 'less-loader'
                    },
                ]
            },
            {
                test: /\.(png|jpg|eot|ttf|svg|woff|woff2|gif)$/,
                use: [
                    {
                        loader: "file-loader",
                        options: {
                            outputPath: 'assets/',
                            name: '[name].[hash].[ext]',
                        }
                    }
                ]
            }
        ]
    },
    plugins: [
        new CleanWebpackPlugin([distPath + "/js", distPath + "/css", distPath + "/assets"]),
        new webpack.NoEmitOnErrorsPlugin(),
        //new ExtractTextPlugin('css/[name].[contenthash].' + (isDevelopment ? 'dev' : 'min') + '.css'),
        new MiniCssExtractPlugin({
            filename: 'css/[name].[contenthash].' + (isDevelopment ? 'dev' : 'min') + '.css',
        }),
        ...bundles.filter(x => x.startsWith("page-")).map((value) => {
            return new HtmlWebpackPlugin({
                filename: path.join(__dirname, '/Views/Shared/Assets/_Gen_' + value + '_Scripts.cshtml'),
                template: path.join(__dirname, '/Views/Shared/Assets/_ScriptsTemplate.cshtml'),
                chunks: ['polyfills', value],
                inject: false,
                chunksSortMode: 'manual',
            })
        }),
        ...bundles.filter(x => x.startsWith("page-")).map((value) => {
            return new HtmlWebpackPlugin({
                filename: path.join(__dirname, '/Views/Shared/Assets/_Gen_' + value + '_Styles.cshtml'),
                template: path.join(__dirname, '/Views/Shared/Assets/_StylesTemplate.cshtml'),
                chunks: ['polyfills', value],
                inject: false,
                chunksSortMode: 'manual',
            })
        }),
        ...bundles.filter(x => !x.startsWith("page-")).map((value) => {
            return new HtmlWebpackPlugin({
                filename: path.join(__dirname, '/Views/Shared/Assets/_Gen_' + value + '_Scripts.cshtml'),
                template: path.join(__dirname, '/Views/Shared/Assets/_ScriptsTemplate.cshtml'),
                chunks: [value],
                inject: false,
                chunksSortMode: 'manual',
            })
        }),
        ...bundles.filter(x => !x.startsWith("page-")).map((value) => {
            return new HtmlWebpackPlugin({
                filename: path.join(__dirname, '/Views/Shared/Assets/_Gen_' + value + '_Styles.cshtml'),
                template: path.join(__dirname, '/Views/Shared/Assets/_StylesTemplate.cshtml'),
                chunks: [value],
                inject: false,
                chunksSortMode: 'manual',
            })
        }),
        ...(isDevelopment ? [] : [
            new UglifyJsPlugin({
                sourceMap: true,
                // Needed for TinyMCE, see https://www.tinymce.com/docs/advanced/usage-with-module-loaders/#minificationwithuglifyjs2
                uglifyOptions: {
                    output: {
                        ascii_only: true,
                    }
                }
            }),
        ])
    ]
};

export default config;