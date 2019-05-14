import { Menu, Icon, Input } from 'ant-design-vue'

const { Item, ItemGroup, SubMenu } = Menu
const { Search } = Input

export default {
  name: 'Tree',
  props: {
    dataSource: {
      type: Array,
      required: true
    },
    openKeys: {
      type: Array,
      default: () => []
    },
    defaultSelectedKeys: {
      type: Array,
      default: () => []
    },
    search: {
      type: Boolean,
      default: false
    },
    addAction: {
      type: String,
      default: null
    }
  },
  watch: {
    dataSource(newVal, oldVal) {
      // 有选中项，默认触发点击事件，以激活数据查询
      if (!this.defaultSelectedKeys.length && newVal.length) {
        this.defaultSelectedKeys.push(newVal[0].key)
      }
    }
  },
  created() {
    this.localOpenKeys = this.openKeys.slice(0)
  },
  data() {
    return {
      localOpenKeys: [],
      searchValue: null,
      item: null,
    }
  },
  methods: {
    handlePlus(item) {
      this.$emit('add', item)
    },
    handleTitleClick(...args) {
      this.$emit('titleClick', { args })
    },
    handleClick(item) {
      this.searchValue = null
      this.item = item
      item.search = this.searchValue
      this.$emit('click', item)
    },
    handleSearch(e) {
      if (e.keyCode !== 13) {
        return
      }
      if (!this.item) {
        return
      }
      let item = this.item
      item.search = this.searchValue
      this.$emit('click', item)
    },
   
    renderSearch() {
      return (
        <Search
          placeholder="搜索"
          v-model={this.searchValue}
          style="width: 100%; margin-bottom: 1rem"
          {... { on: { "keyup": (e) => { this.handleSearch(e) } } }}
        />
      )
    },
    renderIcon(icon) {
      return icon && (<Icon type={icon} />) || null
    },
    renderMenuItem(item) {
      return (
        <Item key={item.key} >
          {this.renderIcon(item.icon)}
          {item.title}
          <a v-action={this.addAction} class="btn" style="width: 20px;z-index:1300" {...{ on: { click: () => this.handlePlus(item) } }}><a-icon type="plus" /></a>
        </Item>
      )
    },
    renderItem(item) {
      return item.children ? this.renderSubItem(item, item.key) : this.renderMenuItem(item, item.key)
    },
    renderItemGroup(item) {
      const childrenItems = item.children.map(o => {
        return this.renderItem(o, o.key)
      })

      return (
        <ItemGroup key={item.key}>
          <template slot="title">
            <span>{item.title}</span>
            <a-dropdown>
              <a class="btn"><a-icon type="ellipsis" /></a>
              <a-menu slot="overlay">
                <a-menu-item key="1">新增</a-menu-item>
                <a-menu-item key="2">合并</a-menu-item>
                <a-menu-item key="3">移除</a-menu-item>
              </a-menu>
            </a-dropdown>
          </template>
          {childrenItems}
        </ItemGroup>
      )
    },
    renderSubItem(item, key) {
      const childrenItems = item.children && item.children.map(o => {
        return this.renderItem(o, o.key)
      })

      const title = (
        <span slot="title">
          {this.renderIcon(item.icon)}
          <span>{item.title}</span>
        </span>
      )

      if (item.group) {
        return this.renderItemGroup(item)
      }
      // titleClick={this.handleTitleClick(item)}
      return (
        <SubMenu key={key}>
          {title}
          {childrenItems}
        </SubMenu>
      )
    }
  },
  render() {
    const { dataSource, search } = this.$props

    // this.localOpenKeys = openKeys.slice(0)
    const list = dataSource.map(item => {
      return this.renderItem(item)
    })

    return (
      <div class="tree-wrapper">
        {search ? this.renderSearch() : null}
        <Menu mode="inline" class="custom-tree" {...{ on: { click: item => this.handleClick(item), 'update:openKeys': val => { this.localOpenKeys = val } } }} openKeys={this.localOpenKeys} defaultSelectedKeys={this.defaultSelectedKeys}>
          {list}
        </Menu>
      </div>
    )
  }
}
